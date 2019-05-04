using System.Web.Mvc;
using CMS.DocumentEngine;

namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    public interface IPageBuilderInitializer
    {
        void Initialize(Controller controller, TreeNode treeNode);
    }
}
