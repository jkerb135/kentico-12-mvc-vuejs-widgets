using System;
using CMS.MediaLibrary;

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections
{
    public interface IMediaFileQuery
    {
        MediaFileInfo GetMediaFile(Guid mediaFileId, string siteName);
    }
}
