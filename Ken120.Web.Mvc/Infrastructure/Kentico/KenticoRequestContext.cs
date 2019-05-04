using System;

namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    public class KenticoCmsRequestContext : ICmsRequestContext
    {
        public string SiteName { get; }
        public int SiteId { get; }
        public string CmsHost { get; }
        public bool IsLatestVersionEnabled { get; }

        public KenticoCmsRequestContext(
            string siteName,
            int siteId,
            string cmsHost,
            bool isLatestVersionEnabled)
        {
            if (cmsHost is null)
            {
                throw new ArgumentNullException(nameof(cmsHost));
            }

            if (siteName is null)
            {
                throw new ArgumentNullException(nameof(siteName));
            }

            SiteName = siteName;
            SiteId = siteId;
            CmsHost = cmsHost;
            IsLatestVersionEnabled = isLatestVersionEnabled;
        }
    }
}
