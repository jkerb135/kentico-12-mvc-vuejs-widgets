using System;

namespace Ken120.Web.Mvc.Infrastructure.Caching
{
    public interface ICmsCacheSettings
    {
        bool IsCacheEnabled { get; }

        TimeSpan CacheItemDuration { get; }
    }
}
