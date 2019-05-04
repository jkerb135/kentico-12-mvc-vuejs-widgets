using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Ken120.Web.Mvc.Infrastructure.Caching;

namespace Ken120.Web.Mvc.Configuration.Dependencies.Registrations
{
    public static class ApplicationRegistration
    {
        public static ContainerBuilder RegisterApplicationTypes(this ContainerBuilder builder)
        {
            // Register queries
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Query", StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(CachingQueryDecorator))
                .InstancePerRequest();

            return builder;
        }
    }
}
