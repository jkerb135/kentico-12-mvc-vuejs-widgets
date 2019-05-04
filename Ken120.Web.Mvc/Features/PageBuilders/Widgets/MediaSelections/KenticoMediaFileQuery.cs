using System;
using CMS.MediaLibrary;

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections
{
    public class KenticoMediaFileQuery : IMediaFileQuery
    {
        public MediaFileInfo GetMediaFile(Guid mediaFileId, string siteName) =>
          MediaFileInfoProvider.GetMediaFileInfo(mediaFileId, siteName);
    }
}
