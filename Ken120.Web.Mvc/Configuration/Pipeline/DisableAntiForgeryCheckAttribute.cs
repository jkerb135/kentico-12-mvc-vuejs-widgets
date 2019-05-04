using System;
using System.Web.Mvc;

namespace Ken120.Web.Mvc.Configuration.Pipeline
{
    /// <summary>
    /// Disables anti forgery validation for the given Action
    /// https://stackoverflow.com/a/36140896/939634
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DisableAntiForgeryCheckAttribute : FilterAttribute
    {
    }
}
