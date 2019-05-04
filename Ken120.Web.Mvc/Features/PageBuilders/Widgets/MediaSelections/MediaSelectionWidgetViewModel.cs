using System;

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections
{
    public class MediaSelectionWidgetViewModel
    {
        public string PermanentUrl { get; set; }
        public string Title { get; set; }
        public int MaxSideSize { get; set; }
        public Guid MediaLibraryItemGuid { get; set; }
        public string HostUrl { get; set; }
    }
}
