using System.Web.Mvc;

using Ken120.Web.Mvc.Controllers.Sections;

using Kentico.PageBuilder.Web.Mvc;

using static Ken120.Web.Mvc.Controllers.Sections.SingleColumnSectionControllerMeta;

[assembly: RegisterSection(IDENTIFIER, typeof(SingleColumnSectionController), NAME, Description = DESCRIPTION, IconClass = ICON_CLASS)]

namespace Ken120.Web.Mvc.Controllers.Sections
{
    internal static class SingleColumnSectionControllerMeta
    {
        public const string IDENTIFIER = "Ken120.Web.Mvc.SingleColumnSection";
        public const string NAME = "Single Column Section";
        public const string DESCRIPTION = "Single Column Section";
        public const string ICON_CLASS = "icon-square";
    }

    public class SingleColumnSectionController : Controller
    {
        public SingleColumnSectionController()
        {

        }

        public ActionResult Index() => PartialView("Sections/_SingleColumnSection");
    }
}
