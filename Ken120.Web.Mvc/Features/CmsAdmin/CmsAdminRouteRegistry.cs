using System.Web.Mvc;
using System.Web.Routing;
using Ken120.Web.Mvc.Configuration.Pipeline;
using Ken120.Web.Mvc.Infrastructure;

namespace Ken120.Web.Mvc.Features.CmsAdmin
{
    public class CmsAdminRouteRegistry : IRouteRegistry
    {
        public int Priority => 1;

        public void RegisterRoutes(RouteCollection routes) =>
            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new
                {
                    controller = typeof(CmsAdminRedirectController).RemoveControllerSuffix(),
                    action = nameof(CmsAdminRedirectController.Index)
                }
            );
    }
}
