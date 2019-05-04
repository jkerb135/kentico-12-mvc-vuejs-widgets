namespace Ken120.Web.Mvc.Infrastructure.Kentico
{
    /// <summary>
    /// Base class for inline editor view models.
    /// </summary>
    public abstract class InlineEditorViewModel
    {
        /// <summary>
        /// Name of the widget property to edit.
        /// </summary>
        public string PropertyName { get; set; }
    }
}
