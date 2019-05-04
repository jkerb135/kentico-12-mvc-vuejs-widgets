using Ken120.Web.Mvc.Controllers.Sections;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public class ApplicationConfig
    {
        public static void RegisterFeatures(IApplicationBuilder builder)
        {
            builder.UsePreview();

            builder.UsePageBuilder(new PageBuilderOptions()
            {
                DefaultSectionIdentifier = SingleColumnSectionControllerMeta.IDENTIFIER,
                RegisterDefaultSection = false
            });

            builder.UseResourceSharingWithAdministration();
        }
    }
}
