using System;

namespace Ken120.Web.Mvc.Features.Sites
{
    public class DefaultSiteMetaService<TSiteMeta> : ISiteMetaService<TSiteMeta> where TSiteMeta : ISiteMeta
    {
        private readonly Func<TSiteMeta> metaFactory;
        private TSiteMeta siteMeta;

        public DefaultSiteMetaService(Func<TSiteMeta> metaFactory) =>
            this.metaFactory = metaFactory;

        public TSiteMeta Get() =>
            siteMeta == null
                ? metaFactory()
                : siteMeta;

        public void Set(TSiteMeta siteMeta)
        {
            if (siteMeta == null)
            {
                throw new ArgumentNullException(nameof(siteMeta));
            }

            this.siteMeta = siteMeta;
        }
    }
}
