using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Helpers;
using CollectorsCantina.Web.Models.Collections;

namespace CollectorsCantina.Web.Services.Collections
{
    public class CollectionsService : ICollectionsService
    {
        private readonly IApiHelper _api;

        public CollectionsService(IApiHelper api)
        {
            _api = api;
        }

        public async Task<CollectionsViewModel> LoadModel()
        {
            var model = new CollectionsViewModel();

            var collections = await _api.Get<List<Collection>>(ApiCatalog.Collection.GetAll);
            model.Collections = CollectionMapper.MapToModel(collections).OrderBy(x => x.Name).ToList();

            return model;
        }

        public async Task<CollectionsViewModel> LoadFavorites()
        {
            var model = new CollectionsViewModel();

            var collections = await _api.Get<List<Collection>>(ApiCatalog.Collection.GetAll);
            model.Collections = CollectionMapper.MapToModel(collections).Where(x => x.IsFavorite).OrderBy(x => x.Name).ToList();

            return model;
        }
    }
}
