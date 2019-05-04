using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using CMS.AspNet.Platform;
using CMS.ContactManagement;
using Ken120.Web.Mvc.Configuration.Dependencies;
using Ken120.Web.Mvc.Configuration.Pipeline;
using Kentico.Web.Mvc;

namespace Ken120.Web.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            /**
             * Enables and configures selected Kentico ASP.NET MVC integration features
             */
            ApplicationConfig.RegisterFeatures(ApplicationBuilder.Current);

            var container = DependencyResolverConfig.Register();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes, container);

            WebApiConfig.ConfigureWebApi(container);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FilterConfig.RegisterGlobalFilters(FilterProviders.Providers, GlobalFilters.Filters, container);
        }

        /// <summary>
        /// Occurs when an unhandled exception in application is thrown.
        /// </summary>
        protected void Application_Error()
        {
            ApplicationErrorLogger.LogLastApplicationError();

            var error = Server.GetLastError();
            if ((error as HttpException)?.GetHttpCode() == 404)
            {
                Server.ClearError();
                Response.StatusCode = 404;
            }
        }

        /// <summary>
        /// Overrides basic application-wide implementation of the <see cref="P:System.Web.UI.PartialCachingAttribute.VaryByCustom" /> property.
        /// </summary>
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            var parameters = custom.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                                   .OrderBy(p => p);

            var parts = new List<string>();
            foreach (string parameter in parameters)
            {
                switch (parameter)
                {
                    case "User":
                        parts.Add($"User={context.User.Identity.Name}");
                        break;

                    case "Persona":
                        // Gets the current contact, without creating a new anonymous contact for new visitors
                        var existingContact = ContactManagementContext.GetCurrentContact(createAnonymous: false);
                        int? contactPersonaID = existingContact?.ContactPersonaID;
                        parts.Add($"Persona={contactPersonaID}|{context.User.Identity.Name}");
                        break;

                    case "Host":
                        parts.Add($"Host={context.Request.GetEffectiveUrl().Host}");
                        break;
                }
            }

            if (parts.Count > 0)
            {
                return string.Join("|", parts);
            }

            return base.GetVaryByCustomString(context, custom);
        }
    }
}
