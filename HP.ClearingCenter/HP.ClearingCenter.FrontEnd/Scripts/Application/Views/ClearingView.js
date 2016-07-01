"use strict";

function ClearingView(parameters) {
    this.options = parameters;    
}
ClearingView.prototype = {
    show: function () {
        var me = this;
        
        if (me.options.isClearingLocked) {
            jQuery('input[type=text], textarea, select').attr('disabled', 'disabled');
            return false;
        }

        var searchText = jQuery('#selectedProductSearchText');
        var searchLabel = jQuery('#selectedProductSearchLabel');
        var loader = jQuery('.autocomplete-loader');
        var changeSelectedProduct = jQuery('#changeSelectedProduct');

        // hidden fields
        var clearReceivedProductForm = jQuery('#clearReceivedProductForm');
        var clearingSelectedProduct = jQuery('#clearingSelectedProduct', clearReceivedProductForm);
        var productId = jQuery('#productId', clearReceivedProductForm);
        var manufacturer = jQuery('#manufacturer', clearReceivedProductForm);
        var productName = jQuery('#productName', clearReceivedProductForm);
        var productNumber = jQuery('#productNumber', clearReceivedProductForm);
        var clearingStatus = jQuery('#clearingStatus', clearReceivedProductForm);
        var clearingRemarks = jQuery('#clearingRemarks', clearReceivedProductForm);

        var submitButton = jQuery('#submitButton');
        var validateProductButton = jQuery('#validateProductButton');
        
        loader.hide();
        searchLabel.hide();
        changeSelectedProduct.hide();

        changeSelectedProduct.click(function () {
            enableSelectProduct(false);
        });

        searchText.autocomplete({
            delay: 300,
            minLength: 1,
            source: me.options.searchProductsJsonUrl,
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    enableSelectProduct(false);
                    submitButton.attr('disabled', true);
                }                

                //hide loading indicator
                loader.hide();
                submitButton.attr('disabled', false);
            },
            search: function (event, ui) {
                //show busy indicator
                loader.show();
                submitButton.attr('disabled', true);
            },
            open: function (event, ui) {
                jQuery('.loading-gif-div').hide();
                submitButton.attr('disabled', true);
                //hide loading indicator
            },
            select: function (event, ui) {
                loader.hide();
                submitButton.attr('disabled', false);
                clearingSelectedProduct.val(ui.item.value);
                enableSelectProduct(true);
                setHiddenFields(ui.item.id);
            }
        });

        searchText.blur(function () {
            var selectedProductId = productId.val();
            if (!isValidProductSelection(selectedProductId)) {
                searchText.val('');
                searchText.focus();
            }
        });

        var modal = jQuery('#dialog-form').dialog({
            autoOpen: false,            
            width: 640,
            modal: true,
            closeOnEscape: true,
            draggable: false,
            close: function () {
                validateProductButton.show();
            }
        });

        jQuery('#cancelManualClearingStatusButton').click(function () {
            modal.dialog('close');
            validateProductButton.show();
            return false;
        });

        jQuery('#submitManualClearingStatusButton').click(function () {
            var clearingRemarksManual = jQuery('#clearingRemarksManual');
            var clearingStatusDropdown = jQuery('#clearingStatusDropdown');

            clearingStatus.val(clearingStatusDropdown.val());
            clearingRemarks.val(clearingRemarksManual.val());
            clearReceivedProductForm.submit();
            modal.dialog('close');

            return false;
        });

        validateProductButton.click(function () {
            var selectedProductId = productId.val();
            if (!isValidProductSelection(selectedProductId)) {
                searchText.val('');
                searchText.focus();
                return false;
            }

            var validateClearingProductQuery = {
                'TransportNumber': jQuery('#transportNumber').val(),                
                'SelectedProductId': selectedProductId
            };

            validateProductButton.hide();

            jQuery.ajax({
                url: me.options.validateSelectedProductUrl,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                accepts: 'application/json',
                data: JSON.stringify(validateClearingProductQuery),
                success: function(data, status, xhr) {
                    if (data.IsValid) {
                        clearingStatus.val(me.options.successfulClearingStatusCode);
                        clearReceivedProductForm.submit();
                    }
                    else {
                        // show modal                        
                        modal.dialog('open');
                    }
                }
            });
            
            // stop event propagation
            return false;
        });

        if (this.options.isProductCleared) {
            searchText.prop("disabled", true);
            changeSelectedProduct.show();
        }

        // private functions
        function isValidProductSelection(productId) {
            var invalidVal = !productId || productId.length == 0 || productId === 0 || productId == '';
            return !invalidVal;
        }

        function enableSelectProduct(isEnabled) {
            if (isEnabled) {
                changeSelectedProduct.show();
                searchText.prop("disabled", true);
            }
            else {
                changeSelectedProduct.hide();
                searchText.val('');
                searchText.prop("disabled", false);
                clearingSelectedProduct.val('');
                setHiddenFields();
            }
        }

        function setHiddenFields(jsonString) {
            var data = jsonString ? JSON.parse(jsonString) : {};
                        
            productId.val(data.ProductId);
            manufacturer.val(data.Manufacturer);
            productName.val(data.ProductName);
            productNumber.val(data.ProductNumber);
        }
    }
};
