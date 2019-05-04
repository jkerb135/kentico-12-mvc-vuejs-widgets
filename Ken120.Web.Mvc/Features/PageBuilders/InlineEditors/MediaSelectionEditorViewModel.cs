using System;
using Ken120.Web.Mvc.Infrastructure.Kentico;

namespace Ken120.Web.Mvc.Features.PageBuilders.InlineEditors
{
    public class MediaSelectionEditorViewModel : InlineEditorViewModel
    {
        public string PermanentUrl { get; set; }
        public string Title { get; set; }
        public Guid MediaLibraryItemGuid { get; set; }
        public string HostUrl { get; set; }
    }
}
