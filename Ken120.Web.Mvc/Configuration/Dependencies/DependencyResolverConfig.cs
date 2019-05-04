using Autofac;
using Ken120.Web.Mvc.Configuration.Dependencies.Registrations;

namespace Ken120.Web.Mvc.Configuration.Dependencies
{
    /// <summary>
    /// Registers required implementations to the Autofac container and set the container as ASP.NET MVC dependency resolver
    /// </summary>
    public static class DependencyResolverConfig
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            var container = builder
                .RegisterApplicationTypes()
                .RegisterMvcTypes()
                .RegisterCmsTypes()
                .RegisterApiTypes()
                .Build();

            return container;
        }
    }
}
