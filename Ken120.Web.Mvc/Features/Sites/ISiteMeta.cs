using System.Collections.Generic;

namespace Ken120.Web.Mvc.Features.Sites
{
    public interface ISiteMeta
    {
        string Title { get; }
        Dictionary<string, string> Metas { get; }
    }
}
