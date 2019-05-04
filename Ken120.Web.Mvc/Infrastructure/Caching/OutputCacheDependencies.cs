using System.Collections.Generic;
using System.Web;

using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;

namespace Ken120.Web.Mvc.Infrastructure.Caching
{
    /// <summary>
    /// Creates a minimum set of ASP.NET output cache dependencies for views that contain data from pages or info objects.
    /// </summary>
    public sealed class OutputCacheDependencies : IOutputCacheDependencies
    {
        private readonly HttpResponseBase response;
        private readonly IContentItemMetadataProvider contentItemMetadataProvider;
        private readonly bool isCacheEnabled;

        private readonly HashSet<string> dependencyCacheKeys = new HashSet<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputCacheDependencies"/> class.
        /// </summary>
        /// <param name="response">HTTP response that will be used to create output cache dependencies.</param>
        /// <param name="contentItemMetadataProvider">object that provides information about pages and info objects using their runtime type.</param>
        /// <param name="isCacheEnabled">Indicates whether caching is enabled.</param>
        public OutputCacheDependencies(
            HttpResponseBase response,
            IContentItemMetadataProvider contentItemMetadataProvider,
            ICmsCacheSettings cacheSettings)
        {
            this.response = response;
            this.contentItemMetadataProvider = contentItemMetadataProvider;
            isCacheEnabled = cacheSettings.IsCacheEnabled;
        }


        /// <summary>
        /// Adds a minimum set of ASP.NET output cache dependencies for a view that contains data from pages of the specified runtime type.
        /// When any page of the specified runtime type is created, updated or deleted, the corresponding output cache item is invalidated.
        /// </summary>
        /// <typeparam name="T">Runtime type that represents pages, i.e. it is derived from the <see cref="TreeNode"/> class.</typeparam>
        public void AddDependencyOnPages<T>() where T : TreeNode, new()
        {
            if (!isCacheEnabled)
            {
                return;
            }

            string className = contentItemMetadataProvider.GetClassNameFromPageRuntimeType<T>();
            string dependencyCacheKey = string.Format("nodes|{0}|{1}|all", SiteContext.CurrentSiteName.ToLowerInvariant(), className);

            AddCacheItemDependency(dependencyCacheKey);
            AddCacheItemDependency("cms.adhocrelationship|all");
            AddCacheItemDependency("cms.relationship|all");
        }


        /// <summary>
        /// Adds a minimum set of ASP.NET output cache dependencies for a view that contains data from info objects of the specified runtime type.
        /// When any info object of the specified runtime type is created, updated or deleted, the corresponding output cache item is invalidated.
        /// </summary>
        /// <typeparam name="T">Runtime type that represents info objects, i.e. it is derived from the <see cref="AbstractInfo{TInfo}"/> class.</typeparam>
        public void AddDependencyOnInfoObjects<T>() where T : AbstractInfo<T>, new()
        {
            if (!isCacheEnabled)
            {
                return;
            }

            string objectType = contentItemMetadataProvider.GetObjectTypeFromInfoObjectRuntimeType<T>();
            string dependencyCacheKey = string.Format("{0}|all", objectType);
            AddCacheItemDependency(dependencyCacheKey);
        }


        private void AddCacheItemDependency(string dependencyCacheKey)
        {
            if (dependencyCacheKeys.Contains(dependencyCacheKey))
            {
                return;
            }

            dependencyCacheKeys.Add(dependencyCacheKey);
            CacheHelper.EnsureDummyKey(dependencyCacheKey);
            response.AddCacheItemDependency(dependencyCacheKey);
        }
    }
}
