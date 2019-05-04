using System.IO;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Ken120.Web.Mvc.Views.ViewHelpers
{
    /// <summary>
    /// Extension methods for <see cref="HtmlHelper{TModel}"/> class.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Ensures AntiForgeryToken is applied to the generate form
        /// https://stackoverflow.com/a/36140896/939634
        /// </summary>
        /// <param name="html"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static MvcForm BeginSecureForm(this HtmlHelper html, string action, string controller)
        {
            var form = html.BeginForm(action, controller);

            html.ViewContext.Writer.Write(html.AntiForgeryToken().ToHtmlString());

            return form;
        }

        public static string GenerateUniqueId(this HtmlHelper html, string originalIdentifier) =>
            $"{originalIdentifier}{Path.GetRandomFileName().Replace(".", string.Empty)}";
    }
}
