'use strict';

function ProductGroupDetailsView(parameters) {
    this.options = parameters;
}
(function($) {
    ProductGroupDetailsView.prototype = {
        show: function () {
            var me = this;
            var undefined;

            $('.removeCategoryLink').click(function () {
                var removeCategoryCommand = {
                    ProductGroupCategoryId: jQuery(this).attr('data-productgroupcategory-id'),
                    CategoryId: jQuery(this).attr('data-category-id'),
                    ProductGroupId: jQuery(this).attr('data-productgroup-id')
                };

                jQuery.ajax({
                    url: me.options.removeCategoryUrl,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    accepts: 'application/json',
                    data: JSON.stringify(removeCategoryCommand),
                    success: function (data, status, xhr) {
                        location.reload();
                    }
                });
            });

            $('.addFilterAttributeForm').each(function (idx, elem) {
                var self = this;
                self.productGroupCategoryId = $('input[name="ProductGroupCategoryId"]', self);
                self.productGroupId = $('input[name="ProductGroupId"]', self);
                self.productGroupExternalCode = $('input[name="ProductGroupExternalCode"]', self);
                self.categoryId = $('input[name="CategoryId"]', self);
                self.attrSelector = $('.attributeSelector', self);
                self.oprSelector = $('.operatorSelector', self);
                self.valueContainer = $('.values', self);
                self.addFilterButton = $('button[type="submit"]', self);                
                self.data = {};

                self._renderValueInputs = function () {
                    if (self.data.CustomAttributeType === undefined) return;

                    self.valueContainer.html('');

                    var optionListItems = self.data.OptionListItems || [];

                    if (optionListItems.length > 0) {
                        var listItemSelector = $('<select name="InputValues"></select>');

                        optionListItems.forEach(function (val) {
                            var option = $('<option></option>');
                            option.attr('value', val.ValueText);
                            option.html(val.DisplayText);
                            listItemSelector.append(option);
                        });

                        self.valueContainer.append(listItemSelector);

                        return;
                    }

                    switch (self.data.CustomAttributeType) {
                        case 0: // unassigned
                        case 1: // string
                            self.valueContainer.append('<input type="text" name="InputValues" />');
                            break;
                        case 2: // integer
                        case 3: // decimal
                            var selectedOperatorId = self.oprSelector.val();
                            var inputHtml = self.data.CustomAttributeType == 2 ? '<input type="number" step="1" name="InputValues" />' : '<input type="number" step="0.01" name="InputValues" />';

                            // if using the BETWEEN operator, show two inputs
                            if (selectedOperatorId === '6') {
                                self.valueContainer.append($(inputHtml).css('width', '50px'));
                                self.valueContainer.append('<span> and </span>');
                                self.valueContainer.append($(inputHtml).css('width', '50px'));
                            }
                            else {
                                self.valueContainer.append($(inputHtml));
                            }

                            break;
                        case 4: // boolean
                            var boolSelector = $('<select name="InputValues"></select>');
                            boolSelector.append('<option value="true">true</option>');
                            boolSelector.append('<option value="false">false</option>');
                            self.valueContainer.append(boolSelector);
                            break;
                    }
                };

                self.attrSelector.change(function (evt) {
                    var customAttributeId = self.attrSelector.val() || '';

                    if (customAttributeId.length == 0) {
                        self.oprSelector.hide();
                        self.addFilterButton.hide();
                        self.valueContainer.html('');
                        return;
                    }

                    // fetch custom attribute info from server
                    $.ajax({
                        url: me.options.getAttributeDetailsUrl,
                        method: 'GET',
                        contentType: 'application/json',
                        accepts: 'application/json',
                        data: 'customAttributeId=' + customAttributeId
                    }).success(function (data, status, xhr) {
                        self.data = data;

                        // clear operators
                        self.oprSelector.hide();
                        self.oprSelector.empty();

                        var allowableOperators = [];
                        if (self.data.OptionListItems && self.data.OptionListItems.length > 0) {
                            data.Operators.forEach(function (op) {
                                if (op.Id == '1' || op.Id == '2') {
                                    allowableOperators.push(op);
                                }
                            });
                        }
                        else {
                            allowableOperators = data.Operators;
                        }

                        // setup operators
                        allowableOperators.forEach(function (val) {
                            var option = jQuery('<option></option>');
                            option.attr('value', val.Id);
                            option.html(val.ShortName);
                            self.oprSelector.append(option);
                        });

                        self.oprSelector.show();
                        self.addFilterButton.show();
                        self._renderValueInputs();
                    });
                });

                self.oprSelector.change(function () {
                    self._renderValueInputs();
                });

                self.addFilterButton.click(function () {
                    $(this).hide();
                    var form = $('<form method="post" class="hidden"></form>');
                    form.attr('action', me.options.addCategoryAttributeFilterUrl);
                    form.append(self.productGroupCategoryId);
                    form.append(self.productGroupId);
                    form.append(self.productGroupExternalCode);
                    form.append(self.categoryId);
                    form.append(self.attrSelector);
                    form.append(self.oprSelector);
                    form.append(self.valueContainer);

                    $(this).parent().append(form);
                    form.submit();

                    return false;
                });
            });
        }
    };
})(jQuery);
