namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    public interface ICmsRequestContext
    {
        string SiteName { get; }
        int SiteId { get; }
        string CmsHost { get; }
        bool IsLatestVersionEnabled { get; }
    }
}
