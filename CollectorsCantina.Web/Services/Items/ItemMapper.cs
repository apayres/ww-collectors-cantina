using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Models.Items;

namespace CollectorsCantina.Web.Services.Items
{
    public static class ItemMapper
    {
        public static ItemViewModel MapToModel(Item item)
        {
            var model = new ItemViewModel()
            {
                CollectionId = item.CollectionId,
                Description = item.Description,
                Images = item.Images?.OrderBy(x => x.DisplayOrder).Select(x => x.ImageUrl).ToList(),
                ItemId = item.ItemId,
                Name = item.Name,
                Notes = item.Notes,
                Tags = item.Tags,
                Type = item.Type,
                Upc = item.Upc
            };

            return model;
        }

        public static List<ItemViewModel> MapToModel(List<Item> items)
        {
            return items.Select(x => MapToModel(x)).ToList();
        }

        public static Item MapToEntity(ItemViewModel model)
        {
            var images = new List<ItemImages>();

            if (model.Images != null)
            {
                for (var i = 0; i < model.Images.Count(); i++)
                {
                    images.Add(new ItemImages()
                    {
                        ImageUrl = model.Images[i],
                        DisplayOrder = i
                    });
                }
            }

            var entity = new Item()
            {
                CollectionId = model.CollectionId,
                Description = model.Description,
                ItemId = model.ItemId,
                Name = model.Name,
                Tags = model.Tags,
                Type = model.Type,
                Notes = model.Notes,
                Upc = model.Upc,
                Attributes = new List<ItemAttributes>(),
                Images = images
            };

            return entity;
        }

    }
}
