using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using Ken120.Web.Mvc.ErrorHandling;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public static class WebApiConfig
    {
        public static HttpConfiguration ConfigureWebApi(IContainer container)
        {
            var config = container.Resolve<HttpConfiguration>();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Remove XML formatter and any others
            config.Formatters.Clear();

            config.Formatters.Add(container.Resolve<JsonMediaTypeFormatter>());

            config.Services.Replace(typeof(IExceptionHandler), container.Resolve<GlobalApiExceptionHandler>());

            // Ensure Web Api is configured correctly (routes, formatting, ect...)
            config.EnsureInitialized();

            return config;
        }
    }
}
