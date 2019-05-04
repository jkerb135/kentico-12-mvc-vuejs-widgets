using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Ken120.Web.Mvc.ErrorHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ken120.Web.Mvc.Configuration.Dependencies.Registrations
{
    public static class ApiRegistration
    {
        public static ContainerBuilder RegisterApiTypes(this ContainerBuilder builder)
        {
            builder
                .RegisterType<GlobalApiExceptionHandler>()
                .SingleInstance();

            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            builder
                .Register(c =>
                {
                    var formatter = new JsonMediaTypeFormatter();
                    var settings = formatter.SerializerSettings;

#if DEBUG
                    settings.Formatting = Formatting.Indented;
#endif

                    // UTC Date serialization configuration
                    settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    settings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

                    // Use 7 digits of precision (fffffff) to match data store datetimeoffset(7)
                    settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffffffK";

                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    return formatter;
                })
                .SingleInstance();

            return builder;
        }
    }
}
