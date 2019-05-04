using System;
using System.Web.Http;
using System.Web.Routing;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public class CoreApiRouteRegistry : IRouteRegistry
    {
        private readonly HttpConfiguration httpConfiguration;

        public int Priority => 10;

        public CoreApiRouteRegistry(HttpConfiguration httpConfiguration)
        {
            if (httpConfiguration is null)
            {
                throw new ArgumentNullException(nameof(httpConfiguration));
            }

            this.httpConfiguration = httpConfiguration;
        }

        public void RegisterRoutes(RouteCollection routes) =>
            httpConfiguration.MapHttpAttributeRoutes();
    }
}
