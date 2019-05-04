using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;

namespace Ken120.Web.Mvc.Infrastructure.Caching
{
    /// <summary>
    /// Provides caching for query methods that return a single content item or a collection of content items. 
    /// Only results of methods starting with 'Get' are cached. A minimum set of cache dependencies is also specified, 
    /// so that when a content item changes, the cached result is invalidated.
    /// </summary>
    public sealed class CachingQueryDecorator : IInterceptor
    {
        /// <summary>
        /// Object that provides information about pages and info objects using their runtime type.
        /// </summary>
        private readonly IContentItemMetadataProvider contentItemMetadataProvider;
        private readonly ICmsCacheSettings cacheSettings;

        public CachingQueryDecorator(
            IContentItemMetadataProvider contentItemMetadataProvider,
            ICmsCacheSettings cacheSettings)
        {
            this.contentItemMetadataProvider = contentItemMetadataProvider;
            this.cacheSettings = cacheSettings;
        }


        /// <summary>
        /// Returns the cached result for the specified method invocation, if possible. Otherwise proceeds with the invocation and caches the result.
        /// Only results of methods starting with 'Get' are affected.
        /// </summary>
        /// <param name="invocation">Method invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            if (!cacheSettings.IsCacheEnabled || !invocation.Method.Name.StartsWith("Get", StringComparison.Ordinal))
            {
                invocation.Proceed();

                return;
            }

            var returnType = invocation.Method.ReturnType;

            var cacheDependencyAttributes = invocation.MethodInvocationTarget.GetCustomAttributes<CacheDependencyAttribute>().ToList();

            if (cacheDependencyAttributes.Count > 0)
            {
                invocation.ReturnValue = GetCachedResult(invocation, GetDependencyCacheKeyFromAttributes(cacheDependencyAttributes, invocation.Arguments));
            }
            else if (typeof(TreeNode).IsAssignableFrom(returnType))
            {
                invocation.ReturnValue = GetCachedResult(invocation, GetDependencyCacheKeyForPage(returnType));
            }
            else if (typeof(IEnumerable<TreeNode>).IsAssignableFrom(returnType))
            {
                invocation.ReturnValue = GetCachedResult(invocation, GetDependencyCacheKeyForPage(returnType.GenericTypeArguments[0]));
            }
            else if (typeof(BaseInfo).IsAssignableFrom(returnType))
            {
                invocation.ReturnValue = GetCachedResult(invocation, GetDependencyCacheKeyForObject(returnType));
            }
            else if (typeof(IEnumerable<BaseInfo>).IsAssignableFrom(returnType))
            {
                invocation.ReturnValue = GetCachedResult(invocation, GetDependencyCacheKeyForObject(returnType.GenericTypeArguments[0]));
            }
            else
            {
                invocation.Proceed();
            }
        }


        private object GetCachedResult(IInvocation invocation, string dependencyCacheKey)
        {
            string cacheKey = GetCacheItemKey(invocation);
            var cacheSettings = CreateCacheSettings(cacheKey, dependencyCacheKey);
            Func<object> provideData = () =>
            {
                invocation.Proceed();
                return invocation.ReturnValue;
            };

            return CacheHelper.Cache(provideData, cacheSettings);
        }


        private string GetCacheItemKey(IInvocation invocation)
        {
            var builder = new StringBuilder(127)
                          .Append("Ken120.Web.Mvc|Data")
                          .Append("|").Append(SiteContext.CurrentSiteName)
                          .Append("|").Append(invocation.TargetType.FullName)
                          .Append("|").Append(invocation.Method.Name)
                          .Append("|").Append(CultureInfo.CurrentCulture.Name);

            foreach (object value in invocation.Arguments)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "|{0}", GetArgumentCacheKey(value));
            }

            return builder.ToString();
        }


        private CacheSettings CreateCacheSettings(string cacheKey, string dependencyCacheKey) =>
            new CacheSettings(cacheSettings.CacheItemDuration.TotalMinutes, cacheKey)
            {
                GetCacheDependency = () => CacheHelper.GetCacheDependency(dependencyCacheKey)
            };


        private string GetDependencyCacheKeyForPage(Type type) =>
            string.Format(PagesCacheDependencyAttribute.KEY_FORMAT, SiteContext.CurrentSiteName.ToLowerInvariant(), contentItemMetadataProvider.GetClassNameFromPageRuntimeType(type));


        private string GetDependencyCacheKeyForObject(Type type) => string.Format("{0}|all", contentItemMetadataProvider.GetObjectTypeFromInfoObjectRuntimeType(type));


        private string GetDependencyCacheKeyFromAttributes(List<CacheDependencyAttribute> attributes, object[] methodArguments) => attributes.Select(attribute => attribute.ResolveKey(SiteContext.CurrentSiteName.ToLowerInvariant(), methodArguments)).Join(TextHelper.NewLine);


        private string GetArgumentCacheKey(object argument)
        {
            if (argument == null)
            {
                return string.Empty;
            }

            if (argument is ICacheKey keyArgument)
            {
                return keyArgument.GetCacheKey();
            }

            return argument.ToString();
        }
    }
}
