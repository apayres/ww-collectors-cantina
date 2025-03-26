using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Helpers;
using CollectorsCantina.Web.Models.Collections;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Services.Collections
{
    public class CollectionService : ICollectionService
    {
        private readonly IApiHelper _api;

        public CollectionService(IApiHelper api)
        {
            _api = api;
        }

        public async Task<CollectionViewModel> LoadModel(Guid? id)
        {
            var model = new CollectionViewModel();

            if (id.HasValue)
            {
                var collection = await _api.Get<Collection>(ApiCatalog.Collection.Get, id);
                model = CollectionMapper.MapToModel(collection);
            }
            else
            {
                model = new CollectionViewModel();
            }

            return model;
        }

        public async Task<CollectionViewModel> ProcessModel(CollectionViewModel model, ProcessModelAction action)
        {
            if (action == ProcessModelAction.Create)
            {
                var newCollection = await _api.Post(ApiCatalog.Collection.Post, CollectionMapper.MapToEntity(model));
                model = CollectionMapper.MapToModel(newCollection);
                return model;
            }

            if (action == ProcessModelAction.Update)
            {
                var updatedCollection = await _api.Put(ApiCatalog.Collection.Put, CollectionMapper.MapToEntity(model));
                model = CollectionMapper.MapToModel(updatedCollection);
                return model;
            }

            if (action == ProcessModelAction.Delete)
            {
                await _api.Delete(ApiCatalog.Collection.Delete, model.CollectionId.Value);
                model = new CollectionViewModel();
                return model;
            }

            return model;
        }

        public async Task<string> UploadImage(IBrowserFile file, string name, string container)
        {
            var filename = name + file.Name.Substring(file.Name.LastIndexOf("."));
            var url = await _api.Upload<string>(ApiCatalog.Collection.Upload, file, filename, container);
            return url;
        }
    }
}
