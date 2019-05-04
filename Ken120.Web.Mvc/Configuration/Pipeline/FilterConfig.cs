using System.Web.Mvc;
using Autofac;
using Ken120.Web.Mvc.ErrorHandling;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(FilterProviderCollection filterProviders, GlobalFilterCollection filters, IContainer container)
        {
            filterProviders.Add(new AntiForgeryTokenFilterProvider());

            filters.Add(new ApplicationInsightsHandleErrorAttribute());
        }
    }
}
