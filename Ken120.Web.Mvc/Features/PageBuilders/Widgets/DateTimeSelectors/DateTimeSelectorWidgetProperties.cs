using System;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Ken120.Web.Mvc.Features.PageBuilders.Widgets.DateTimeSelectors
{
    public class DateTimeSelectorWidgetProperties : IWidgetProperties
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER,
            DefaultValue = "The Date and Time is",
            ExplanationText = "Content appearing before date and time values",
            Label = "Date & Time Explanation",
            Order = 1,
            Tooltip = "Date & Time Explanation")]
        public string DateTimeContent { get; set; }
    }
}
