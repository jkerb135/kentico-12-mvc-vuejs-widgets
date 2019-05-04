using System.Web;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;

namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    public class KenticoCmsPreviewSettings : ICmsPreviewSettings
    {
        public bool IsPreviewEnabled => HttpContext.Current.Kentico().Preview().Enabled;
    }
}
