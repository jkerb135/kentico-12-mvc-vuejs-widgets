using System.Collections.Generic;

namespace Ken120.Web.Mvc.Features.Sites
{
    public class MetaViewModel
    {
        public string Title { get; }
        public Dictionary<string, string> Metas { get; }

        public MetaViewModel(
            string title,
            Dictionary<string, string> metas)
        {
            Title = title;
            Metas = metas;
        }
    }
}
