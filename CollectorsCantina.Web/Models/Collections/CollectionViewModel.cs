namespace CollectorsCantina.Web.Models.Collections
{
    public class CollectionViewModel : PageViewModel
    {
        public Guid? CollectionId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string StartYear { get; set; } = string.Empty;

        public string EndYear { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsFavorite { get; set; } = false;
    }
}
