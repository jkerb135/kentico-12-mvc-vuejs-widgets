using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.PageBuilder.Web.Mvc;

namespace Ken120.Web.Mvc.Views.ViewHelpers
{
    /// <summary>
    /// Provides filter methods to restrict the list of allowed widgets for editable areas.
    /// </summary>
    public static class AreaRestrictionHelper
    {
        /// <summary>
        /// Gets list of widget identifiers allowed for landing page.
        /// </summary>
        public static string[] GetLandingPageRestrictions()
        {
            string[] allowedScopes = new[] { "Kentico.", "Ken120.Web.Mvc.General.", "Ken120.Web.Mvc.LandingPage.", "Ken120.Web.Mvc." };

            return GetWidgetsIdentifiers()
                .Where(id => allowedScopes.Any(scope => id.StartsWith(scope, StringComparison.OrdinalIgnoreCase)))
                .ToArray();
        }


        /// <summary>
        /// Gets list of widget identifiers allowed for home page.
        /// </summary>
        public static string[] GeHomePageRestrictions()
        {
            string[] deniedScopes = new[] { "Ken120.Web.Mvc.LandingPage." };

            return GetWidgetsIdentifiers()
                .Where(id => deniedScopes.Any(scope => !id.StartsWith(scope, StringComparison.OrdinalIgnoreCase)))
                .ToArray();
        }


        private static IEnumerable<string> GetWidgetsIdentifiers() =>
            new ComponentDefinitionProvider<WidgetDefinition>()
                   .GetAll()
                   .Select(definition => definition.Identifier);
    }
}
