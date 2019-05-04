using System.Web.Mvc;
using CMS.Core;

namespace Ken120.Web.Mvc.Features.CmsAdmin
{
    /// <summary>
    /// Redirects a request to "/admin" to the administration site specified in <c>DancingGoatAdminUrl</c> app setting.
    /// </summary>
    public class CmsAdminRedirectController : Controller
    {
        public ActionResult Index()
        {
            string adminUrl = CoreServices.AppSettings["cms:url:admin-redirect"];

            if (!string.IsNullOrEmpty(adminUrl))
            {
                return RedirectPermanent(adminUrl);
            }

            return HttpNotFound();
        }
    }
}
