using Newtonsoft.Json;

namespace CollectorsCantina.Domain.Entities
{
    public class Collection
    {
        [JsonProperty("id")]
        public Guid? CollectionId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int StartYear { get; set; }

        public int? EndYear { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsFavorite { get; set; } = false;
    }
}
