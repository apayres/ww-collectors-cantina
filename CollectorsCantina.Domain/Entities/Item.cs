using CollectorsCantina.Domain.Enums;
using Newtonsoft.Json;

namespace CollectorsCantina.Domain.Entities
{
    public class Item
    {
        [JsonProperty("id")]
        public Guid? ItemId { get; set; }

        [JsonProperty("collectionId")]
        public Guid CollectionId { get; set; }

        public string Upc {  get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public ItemType Type { get; set; }

        public List<ItemImages>? Images { get; set; }

        public List<ItemAttributes>? Attributes { get; set; }

        public List<string>? Tags { get; set; }
    }
}
