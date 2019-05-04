using System.Web.Routing;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    /// <summary>
    /// Sourced from https://benfoster.io/blog/improving-aspnet-mvc-routing-configuration
    /// </summary>
    public interface IRouteRegistry
    {
        /// <summary>
        /// Provides the global <see cref="RouteCollection"/> to which the Registery's routes
        /// should be added
        /// </summary>
        /// <param name="routes"></param>
        void RegisterRoutes(RouteCollection routes);

        /// <summary>
        /// Sets the priority of the registry which is used to determine
        /// the order in which all Registrys in the application should be added to the
        /// global <see cref="RouteCollection"/>. The lowest priority (0) numerically will
        /// be registered first.
        /// </summary>
        int Priority { get; }
    }
}
