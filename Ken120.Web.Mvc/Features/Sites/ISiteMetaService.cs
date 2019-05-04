namespace Ken120.Web.Mvc.Features.Sites
{
    public interface ISiteMetaService<TSiteMeta> where TSiteMeta : ISiteMeta
    {
        TSiteMeta Get();

        void Set(TSiteMeta siteMeta);
    }
}
