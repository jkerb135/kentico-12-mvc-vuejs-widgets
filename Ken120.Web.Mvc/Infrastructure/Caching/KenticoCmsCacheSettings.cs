using System;

namespace Ken120.Web.Mvc.Infrastructure.Caching
{
    public class KenticoCmsCacheSettings : ICmsCacheSettings
    {
        public bool IsCacheEnabled { get; }

        public TimeSpan CacheItemDuration { get; }

        public KenticoCmsCacheSettings(
            TimeSpan cacheItemDuration,
            bool isCacheEnabled)
        {
            CacheItemDuration = cacheItemDuration;
            IsCacheEnabled = isCacheEnabled;
        }
    }
}
