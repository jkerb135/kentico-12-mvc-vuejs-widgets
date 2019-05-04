using System.Web.Mvc;
using CMS.DocumentEngine;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;

namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    public class HttpContextPageBuilderInitializer : IPageBuilderInitializer
    {
        public void Initialize(Controller controller, TreeNode treeNode) => controller.HttpContext.Kentico().PageBuilder().Initialize(treeNode.DocumentID);
    }
}
