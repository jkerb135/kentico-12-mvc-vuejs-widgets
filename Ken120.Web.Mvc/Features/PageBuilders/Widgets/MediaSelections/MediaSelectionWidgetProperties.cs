using System;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections
{
    public class MediaSelectionWidgetProperties : IWidgetProperties
    {
        public Guid MediaLibraryItemGuid { get; set; }

        [EditingComponent(
            IntInputComponent.IDENTIFIER,
            Order = 0,
            Label = "Live Site: Max Side Size",
            DefaultValue = 200,
            ExplanationText = "The maximum length in pixels of either the width or height dimension of the image when displayed on the live site",
            Tooltip = "Number of Pixels")]
        public int MaxSideSize { get; set; }
    }
}
