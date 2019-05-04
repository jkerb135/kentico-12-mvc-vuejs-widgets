using System;
using System.Configuration;
using System.Globalization;
using Autofac;
using CMS.SiteProvider;
using Ken120.Web.Mvc.Infrastructure.Caching;
using Ken120.Web.Mvc.Infrastructure.Kentico;

namespace Ken120.Web.Mvc.Configuration.Dependencies.Registrations
{
    public static class CmsRegistration
    {
        public static ContainerBuilder RegisterCmsTypes(this ContainerBuilder builder)
        {
            builder
                .Register(ctx => new KenticoCmsRequestContext(
                    cmsHost: ConfigurationManager.AppSettings["cms:url:host"],
                    siteId: SiteContext.CurrentSiteID,
                    siteName: SiteContext.CurrentSiteName,
                    isLatestVersionEnabled: ctx.Resolve<ICmsPreviewSettings>().IsPreviewEnabled))
                .As<ICmsRequestContext>()
                .InstancePerRequest();

            builder
                .RegisterType<KenticoCmsPreviewSettings>()
                .As<ICmsPreviewSettings>();

            builder
                .RegisterType<HttpContextPageBuilderInitializer>()
                .As<IPageBuilderInitializer>();

            builder.RegisterType<ContentItemMetadataProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<CachingQueryDecorator>()
                .InstancePerRequest();

            builder.RegisterType<OutputCacheDependencies>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .Register(ctx =>
                {
                    var cacheItemDuration = GetCacheItemDuration();

                    return new KenticoCmsCacheSettings(
                        cacheItemDuration: cacheItemDuration,
                        isCacheEnabled: false);
                })
                .As<ICmsCacheSettings>();

            builder.RegisterSource(new KenticoRegistrationSource());

            return builder;
        }

        private static TimeSpan GetCacheItemDuration()
        {
            string value = ConfigurationManager.AppSettings["cache:query-result:duration"];

            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int seconds) && seconds > 0)
            {
                return TimeSpan.FromSeconds(seconds);
            }

            return TimeSpan.Zero;
        }
    }
}
