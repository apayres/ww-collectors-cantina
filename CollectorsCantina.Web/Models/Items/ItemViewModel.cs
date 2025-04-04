using CollectorsCantina.Domain.Enums;

namespace CollectorsCantina.Web.Models.Items
{
    public class ItemViewModel : PageViewModel
    {
        public Guid? ItemId { get; set; }

        public Guid CollectionId { get; set; }

        public string Upc { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ItemType Type { get; set; }

        public string Notes { get; set; } = string.Empty;

        public List<string>? Tags { get; set; } = new List<string>();

        public string Number { get; set; } = string.Empty;

        public string NewTag { set; get; } = string.Empty;

        public List<string>? Images { get; set; } = new List<string>();

        public ItemMetaDataViewModel? MetaData { get; set; }

        public bool DeleteImageComplete { get; set; }
    }
}
