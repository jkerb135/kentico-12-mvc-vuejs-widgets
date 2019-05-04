using System;
using System.Web.Mvc;
using Ken120.Web.Mvc.Features.Sites;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MetaTagActionFilterAttribute : ActionFilterAttribute
    {
        public ISiteMetaService<SiteMeta> SiteMetaService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var siteMeta = SiteMetaService.Get();

            filterContext.Controller.ViewBag.Title = siteMeta.Title;
            filterContext.Controller.ViewBag.Metas = siteMeta.Metas;

            base.OnActionExecuted(filterContext);
        }
    }
}
