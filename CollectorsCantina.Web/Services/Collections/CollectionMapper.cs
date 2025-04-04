using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Models.Collections;

namespace CollectorsCantina.Web.Services.Collections
{
    public static class CollectionMapper
    {
        public static CollectionViewModel MapToModel(Collection entity)
        {
            return new CollectionViewModel()
            {
                CollectionId = entity.CollectionId,
                Description = entity.Description,
                EndYear = entity.EndYear == null ? string.Empty : entity.EndYear.ToString(),
                ImageUrl = entity.ImageUrl,
                Name = entity.Name,
                IsFavorite = entity.IsFavorite,
                StartYear = entity.StartYear.ToString(),
                Notes = entity.Notes
            };
        }

        public static List<CollectionViewModel> MapToModel(List<Collection> entities)
        {
            return entities.Select(x => MapToModel(x)).ToList();
        }

        public static Collection MapToEntity(CollectionViewModel model)
        {
            return new Collection()
            {
                StartYear = int.Parse(model.StartYear),
                EndYear = string.IsNullOrEmpty(model.EndYear) ? null : int.Parse(model.EndYear),
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                CollectionId = model.CollectionId,
                IsFavorite = model.IsFavorite,
                Description = model.Description,
                Notes = model.Notes
            };
        }
    }
}
