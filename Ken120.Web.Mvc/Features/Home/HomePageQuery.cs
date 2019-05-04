using CMS.DocumentEngine.Types.Ken120MvcSeed;
using Ken120.Web.Mvc.Infrastructure.Kentico;

namespace Ken120.Web.Mvc.Features.Home
{
    public class HomePageQuery : IHomePageQuery
    {
        private readonly ICmsRequestContext cmsRequestContext;

        public HomePageQuery(ICmsRequestContext cmsRequestContext) =>
            this.cmsRequestContext = cmsRequestContext;

        public HomePage GetHomePage() =>
            HomePageProvider.GetHomePages()
                .LatestVersion(cmsRequestContext.IsLatestVersionEnabled)
                .Published(!cmsRequestContext.IsLatestVersionEnabled)
                .OnSite(cmsRequestContext.SiteName)
                .CombineWithDefaultCulture()
                .TopN(1);
    }
}
