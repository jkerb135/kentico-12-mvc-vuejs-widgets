using System.Web.Mvc;
using System.Web.Routing;
using Ken120.Web.Mvc.Configuration.Pipeline;
using Ken120.Web.Mvc.Infrastructure;

namespace Ken120.Web.Mvc.Features.Home
{
    public class HomeRouteRegistry : IRouteRegistry
    {
        public int Priority => 1;

        public void RegisterRoutes(RouteCollection routes) =>
            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = typeof(HomeController).RemoveControllerSuffix(),
                    action = nameof(HomeController.Index),
                    id = UrlParameter.Optional
                }
            );

    }
}
