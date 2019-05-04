using System.Web.Mvc;
using Ken120.Web.Mvc.Features.PageBuilders.Widgets.DateTimeSelectors;
using Kentico.PageBuilder.Web.Mvc;

using static Ken120.Web.Mvc.Features.PageBuilders.Widgets.DateTimeSelectors.CalendarWidgetControllerMeta;

[assembly: RegisterWidget(IDENTIFIER, typeof(DateTimeSelectorWidgetController), NAME, Description = DESCRIPTION, IconClass = ICON_CLASS)]

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.DateTimeSelectors
{
    internal static class CalendarWidgetControllerMeta
    {
        public const string IDENTIFIER = "Ken120.Web.Mvc.General.DateTimeSelectors";
        public const string NAME = "Calendar Widget";
        public const string DESCRIPTION = "Calendar Widget";
        public const string ICON_CLASS = "icon-l-calendar";
    }

    public class DateTimeSelectorWidgetController : WidgetController<DateTimeSelectorWidgetProperties>
    {
        public DateTimeSelectorWidgetController()
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="DateTimeSelectorWidgetController"/> class.
        /// </summary>
        /// <param name="propertiesRetriever">Retriever for widget properties.</param>
        /// <param name="currentPageRetriever">Retriever for current page where is the widget used.</param>
        /// <remarks>Use this constructor for tests to handle dependencies.</remarks>
        public DateTimeSelectorWidgetController(
            IWidgetPropertiesRetriever<DateTimeSelectorWidgetProperties> propertiesRetriever,
            ICurrentPageRetriever currentPageRetriever) : base(propertiesRetriever, currentPageRetriever)
        {
        }

        public ActionResult Index()
        {
            var properties = GetProperties();

            return PartialView("Widgets/_DateTimeSelectorWidget", new DateTimeSelectorWidgetViewModel
            {
                Date = properties.Date,
                Time = properties.Time,
                DateTimeContent = properties.DateTimeContent,
            });
        }
    }
}
