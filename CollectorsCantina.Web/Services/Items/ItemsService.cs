using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Helpers;
using CollectorsCantina.Web.Models.Items;

namespace CollectorsCantina.Web.Services.Items
{
    public class ItemsService : IItemsService
    {
        private readonly IApiHelper _api;

        public ItemsService(IApiHelper api)
        {
            _api = api;
        }

        public async Task<ItemsViewModel> LoadModel(Guid collectionId)
        {
            var VM = new ItemsViewModel();

            var items = await _api.Get<List<Item>>(ApiCatalog.Item.GetByCollection, collectionId);

            VM.Items = ItemMapper.MapToModel(items);

            return VM;
        }

        public async Task<ItemsViewModel> LoadModel(string tag)
        {
            var VM = new ItemsViewModel();

            var items = await _api.Get<List<Item>>(ApiCatalog.Item.Search, tag);

            VM.Items = ItemMapper.MapToModel(items);

            return VM;
        }
    }
}
