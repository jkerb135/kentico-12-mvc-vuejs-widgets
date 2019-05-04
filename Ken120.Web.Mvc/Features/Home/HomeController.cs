using System.Collections.Generic;
using System.Web.Mvc;
using CMS.DocumentEngine.Types.Ken120MvcSeed;
using Ken120.Web.Mvc.Features.Sites;
using Ken120.Web.Mvc.Infrastructure.Caching;
using Ken120.Web.Mvc.Infrastructure.Kentico;

namespace Ken120.Web.Mvc.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IHomePageQuery homePageQuery;
        private readonly IPageBuilderInitializer pageBuilderInitializer;
        private readonly IOutputCacheDependencies outputCacheDependencies;
        private readonly ISiteMetaService<SiteMeta> siteMetaService;

        public HomeController(
            IHomePageQuery homePageQuery,
            IPageBuilderInitializer pageBuilderInitializer,
            IOutputCacheDependencies outputCacheDependencies,
            ISiteMetaService<SiteMeta> siteMetaService)
        {
            this.homePageQuery = homePageQuery;
            this.pageBuilderInitializer = pageBuilderInitializer;
            this.outputCacheDependencies = outputCacheDependencies;
            this.siteMetaService = siteMetaService;
        }

        public ActionResult Index()
        {
            var homePage = homePageQuery.GetHomePage();

            pageBuilderInitializer.Initialize(this, homePage);

            var viewModel = new IndexViewModel(
                greeting: "Welcome to this new site!");

            outputCacheDependencies.AddDependencyOnPages<HomePage>();

            siteMetaService.Set(new SiteMeta(
                title: $"Home - {homePage.DocumentPageTitle}",
                metas: new Dictionary<string, string>()
                {
                    { "A", "B" }
                }
            ));

            return View(viewModel);
        }
    }
}
