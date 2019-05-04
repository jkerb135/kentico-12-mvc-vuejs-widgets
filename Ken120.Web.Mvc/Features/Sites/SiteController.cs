using System.Collections.Generic;
using System.Web.Mvc;

namespace Ken120.Web.Mvc.Features.Sites
{
    public class SiteController : Controller
    {
        private readonly ISiteMetaService<SiteMeta> siteMetaService;

        public SiteController(ISiteMetaService<SiteMeta> siteMetaService) =>
            this.siteMetaService = siteMetaService;

        public PartialViewResult Meta()
        {
            var siteMeta = siteMetaService.Get();

            var metaTags = new Dictionary<string, string>
            {
                { "a","b" }
            };

            var vm = new MetaViewModel(siteMeta.Title, metaTags);

            return PartialView(vm);
        }
    }
}
