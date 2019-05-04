(function () {
    'use strict';

    let selection = undefined;

    window.kentico.pageBuilder.registerInlineEditor("time-editor", {
        init: function (options) {
            selection = $('.time-editor input').timepicker();

            selection.on('changeTime', function (e) {
                const event = new CustomEvent("updateProperty", {
                    detail: {
                        name: options.propertyName,
                        value: $(this).val(),
                        refreshMarkup: false
                    }
                });

                options.editor.dispatchEvent(event);
            });
        },

        destroy: function () {
            selection.timepicker('remove');
        },

        dragStart: function (options) { }
    });
})();
