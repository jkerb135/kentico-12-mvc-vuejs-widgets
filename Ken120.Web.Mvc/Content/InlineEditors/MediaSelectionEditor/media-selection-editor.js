(function () {
  "use strict";

  let component = undefined;

  window.kentico.pageBuilder.registerInlineEditor("media-selection-editor", {
    init: function (options) {
      const editor = options.editor;

      component = new Vue({
        el: `#${editor.attributes["data-id"].value}`,
        data: {
          hostUrl: editor.attributes["data-host-url"].value,
          selectedMediaUrl: editor.attributes["data-current-media"].value,
          propertyName: options.propertyName
        },
        methods: {
          onDispatch: function (media) {
            const widgetEvent = new CustomEvent("updateProperty", {
              detail: {
                name: this.propertyName,
                value: media.fileGUID,
                refreshMarkup: false
              }
            });

            editor.dispatchEvent(widgetEvent);

            this.selectedMediaUrl = media.permanentUrl;
          }
        }
      });
    },

    destroy: function () {
      component.$destroy();
    },

    dragStart: function (options) { }
  });
})();
