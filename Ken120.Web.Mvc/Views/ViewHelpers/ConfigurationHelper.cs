namespace Ken120.Web.Mvc.Views.ViewHelpers
{
    public static class ConfigurationHelper
    {
        public static bool IsReleaseBuild() =>
#if DEBUG
            false;
#else
            true;
#endif
    }
}
