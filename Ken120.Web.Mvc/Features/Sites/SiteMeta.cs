using System;
using System.Collections.Generic;

namespace Ken120.Web.Mvc.Features.Sites
{
    public class SiteMeta : ISiteMeta
    {
        public string Title { get; } = "None";
        public Dictionary<string, string> Metas { get; } = new Dictionary<string, string>();

        public SiteMeta(
            string title,
            Dictionary<string, string> metas)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (metas is null)
            {
                throw new ArgumentNullException(nameof(metas));
            }

            Title = title;
            Metas = metas;
        }
    }
}
