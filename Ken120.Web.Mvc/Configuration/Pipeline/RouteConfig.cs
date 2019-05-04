using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Autofac;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, IContainer container)
        {
            var routeRegistries = container
                .Resolve<IEnumerable<IRouteRegistry>>()
                .OrderBy(r => r.Priority);

            foreach (var routeRegistry in routeRegistries)
            {
                routeRegistry.RegisterRoutes(routes);
            }
        }
    }
}
