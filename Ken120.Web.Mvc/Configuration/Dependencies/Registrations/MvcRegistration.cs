using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ken120.Web.Mvc.Configuration.Pipeline;
using Ken120.Web.Mvc.Features.Sites;

namespace Ken120.Web.Mvc.Configuration.Dependencies.Registrations
{
    public static class MvcRegistration
    {
        public static ContainerBuilder RegisterMvcTypes(this ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("RouteRegistry"))
                .As<IRouteRegistry>();

            Func<SiteMeta> defaultMetaFactory = () =>
                new SiteMeta("", new Dictionary<string, string>());

            builder
                .Register(ctx => new DefaultSiteMetaService<SiteMeta>(defaultMetaFactory))
                .As<ISiteMetaService<SiteMeta>>()
                .InstancePerRequest();

            builder
                .RegisterType<MetaTagActionFilterAttribute>()
                .AsActionFilterFor<Controller>()
                .InstancePerRequest()
                .PropertiesAutowired();

            return builder;
        }
    }
}
