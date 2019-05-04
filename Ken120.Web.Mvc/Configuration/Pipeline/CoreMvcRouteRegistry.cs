using System.Web.Mvc;
using System.Web.Routing;
using Kentico.Web.Mvc;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public class CoreMvcRouteRegistry : IRouteRegistry
    {
        public int Priority => 0;

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Maps routes to Kentico HTTP handlers and features enabled in ApplicationConfig.cs
            // Always map the Kentico routes before adding other routes. Issues may occur if Kentico URLs are matched by a general route, for example images might not be displayed on pages
            routes.Kentico().MapRoutes();

            routes.AppendTrailingSlash = false;
            routes.LowercaseUrls = true;
        }
    }
}
