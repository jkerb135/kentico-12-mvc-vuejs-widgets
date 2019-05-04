using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    /// <summary>
    /// Automates anti forgery validation on POST requests
    /// https://stackoverflow.com/a/36140896/939634
    /// </summary>
    public class AntiForgeryTokenFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = actionDescriptor.GetFilterAttributes(true);

            bool disableAntiForgery = filters.Any(f => f is DisableAntiForgeryCheckAttribute);

            string method = controllerContext.HttpContext.Request.HttpMethod;

            if (!disableAntiForgery && string.Equals(method, HttpMethod.Post.Method, StringComparison.OrdinalIgnoreCase))
            {
                yield return new Filter(new ValidateAntiForgeryTokenAttribute(), FilterScope.Global, null);
            }
        }
    }
}
