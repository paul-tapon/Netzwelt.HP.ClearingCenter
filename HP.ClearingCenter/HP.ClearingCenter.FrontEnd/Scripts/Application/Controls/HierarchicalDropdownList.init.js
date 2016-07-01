'use strict';

(function () {
    jQuery(document).ready(function () {
        var triggerId = '#{0}';
        var contentId = '#{1}';
        var jsCallback = '{2}';

        jQuery(triggerId).menu({
            content: jQuery(contentId).html(),
            callback: 'onCategorySelected'
        });
    });
});
