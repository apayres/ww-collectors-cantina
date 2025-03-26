using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Domain.IUseCases.Items;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Items
{
    public class DeleteItemHandler : IDeleteItemHandler
    {
        private readonly IItemRepository _repository;
        private readonly IFileStorageManager _fileStorageManager;

        public DeleteItemHandler(IItemRepository repository, IFileStorageManager fileStorageManager)
        {
            _repository = repository;
            _fileStorageManager = fileStorageManager;
        }

        public async Task<ApplicationResponse<Item>> Delete(Guid itemId, Guid collectionId)
        {
            var response = new ApplicationResponse<Item>();
            response.Data = null;

            _fileStorageManager.DeleteContainer(collectionId.ToString());

            await _repository.Delete(itemId, collectionId);
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
