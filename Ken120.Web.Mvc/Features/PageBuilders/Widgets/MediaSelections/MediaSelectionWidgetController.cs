using System.Web.Mvc;
using CMS.MediaLibrary;
using Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections;
using Ken120.Web.Mvc.Infrastructure.Kentico;
using Kentico.PageBuilder.Web.Mvc;
using static Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections.MediaSelectionWidgetControllerMeta;

[assembly: RegisterWidget(
  IDENTIFIER,
  typeof(MediaSelectionWidgetController),
  NAME,
  Description = DESCRIPTION,
  IconClass = ICON_CLASS)]

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.MediaSelections
{
    /// <summary>
    /// This meta class is easier to read than putting all the values inline 
    /// in the RegisterWidget attribute above
    /// </summary>
    internal static class MediaSelectionWidgetControllerMeta
    {
        public const string IDENTIFIER = "Ken120.Web.Mvc.General.MediaSelection";
        public const string NAME = "Media Selection Widget";
        public const string DESCRIPTION = "Media Selection Widget";
        public const string ICON_CLASS = "icon-l-media";
    }

    public class MediaSelectionWidgetController : WidgetController<MediaSelectionWidgetProperties>
    {
        private readonly ICmsRequestContext cmsRequestContext;
        private readonly IMediaFileQuery mediaFileQuery;

        public MediaSelectionWidgetController(
          ICmsRequestContext cmsRequestContext,
          IMediaFileQuery mediaFileQuery)
        {
            this.cmsRequestContext = cmsRequestContext;
            this.mediaFileQuery = mediaFileQuery;
        }

        public MediaSelectionWidgetController(
            ICmsRequestContext cmsRequestContext,
            IWidgetPropertiesRetriever<MediaSelectionWidgetProperties> propertiesRetriever,
            ICurrentPageRetriever currentPageRetriever,
            IMediaFileQuery mediaFileQuery)
          : base(propertiesRetriever, currentPageRetriever)
        {
            this.cmsRequestContext = cmsRequestContext;
            this.mediaFileQuery = mediaFileQuery;
        }

        public ActionResult Index()
        {
            var properties = GetProperties();

            if (properties.MediaLibraryItemGuid == default)
            {
                return PartialView("Widgets/_MediaSelectionWidget",
                  new MediaSelectionWidgetViewModel
                  {
                      MaxSideSize = properties.MaxSideSize,
                      MediaLibraryItemGuid = properties.MediaLibraryItemGuid,
                      PermanentUrl = "",
                      Title = "",
                      HostUrl = cmsRequestContext.CmsHost
                  });
            }

            var mediaFileInfo = mediaFileQuery
              .GetMediaFile(
                properties.MediaLibraryItemGuid,
                cmsRequestContext.SiteName);

            var viewModel = new MediaSelectionWidgetViewModel
            {
                MaxSideSize = properties.MaxSideSize,
                HostUrl = cmsRequestContext.CmsHost,
            };

            if (mediaFileInfo is null)
            {
                viewModel.Title = "";
                viewModel.PermanentUrl = "";
                viewModel.MediaLibraryItemGuid = default;
            }
            else
            {
                viewModel.Title = mediaFileInfo.FileTitle;

                viewModel.PermanentUrl = MediaLibraryHelper
                  .GetPermanentUrl(mediaFileInfo)
                  .TrimStart('~');

                viewModel.MediaLibraryItemGuid = properties.MediaLibraryItemGuid;
            }

            return PartialView("Widgets/_MediaSelectionWidget", viewModel);
        }
    }
}
