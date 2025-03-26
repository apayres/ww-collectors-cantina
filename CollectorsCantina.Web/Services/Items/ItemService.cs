using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Helpers;
using CollectorsCantina.Web.Models.Items;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IApiHelper _api;

        public ItemService(IApiHelper api)
        {
            _api = api;
        }

        public async Task<ItemViewModel> LoadModel(Guid? id)
        {
            var model = new ItemViewModel();

            if (id == null)
            {
                return model;
            }

            var item = await _api.Get<Item>(ApiCatalog.Item.Get, id);
            model = ItemMapper.MapToModel(item);
            return model;
        }

        public async Task<ItemViewModel> ProcessModel(ItemViewModel model, ProcessModelAction action)
        {
            if (action == ProcessModelAction.Create)
            {
                var createdItem = await _api.Post(ApiCatalog.Item.Post, ItemMapper.MapToEntity(model));
                return ItemMapper.MapToModel(createdItem);
            }

            if (action == ProcessModelAction.Update)
            {
                var updatedItem = await _api.Put(ApiCatalog.Item.Put, ItemMapper.MapToEntity(model));
                return ItemMapper.MapToModel(updatedItem);
            }

            if (action == ProcessModelAction.Delete)
            {
                await _api.Delete(ApiCatalog.Item.Delete, model.CollectionId.ToString() + "/" + model.ItemId.ToString());

                return new ItemViewModel();
            }

            return model;
        }

        public async Task<string> UploadImage(IBrowserFile file, string name, string container)
        {
            var filename = name + file.Name.Substring(file.Name.LastIndexOf("."));
            var url = await _api.Upload<string>(ApiCatalog.Item.Upload, file, filename, container);
            return url;
        }

    }
}
