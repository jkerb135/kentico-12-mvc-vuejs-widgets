using System.Web.Mvc;

namespace Ken120.Web.Mvc.ErrorHandling
{
    public class HttpErrorsController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}
