using System.Web.Mvc;
using System.Web.Routing;
using Ken120.Web.Mvc.Configuration.Pipeline;
using Ken120.Web.Mvc.Infrastructure;

namespace Ken120.Web.Mvc.ErrorHandling
{
    public class HttpErrorsRouteRegistry : IRouteRegistry
    {
        public int Priority => 1;

        public void RegisterRoutes(RouteCollection routes) =>
            routes.MapRoute(
                name: "HttpErrors",
                url: "HttpErrors/{action}",
                defaults: new
                {
                    controller = typeof(HttpErrorsController).RemoveControllerSuffix(),
                    action = nameof(HttpErrorsController.ServerError)
                }
            );
    }
}
