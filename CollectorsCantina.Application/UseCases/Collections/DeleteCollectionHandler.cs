using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IUseCases.Collections;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Collections
{
    public class DeleteCollectionHandler : IDeleteCollectionHandler
    {
        private readonly ICollectionRepository _repository;
        private readonly IItemRepository _itemRepository;

        public DeleteCollectionHandler(ICollectionRepository repository, IItemRepository itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
        }

        public async Task<ApplicationResponse<Collection>> Delete(Guid collectionId)
        {
            var response = new ApplicationResponse<Collection>();
            response.Data = null;

            var collectionItems = await _itemRepository.GetByCollection(collectionId);
            if (collectionItems.Any())
            {
                response.Status = ResponseStatus.Fail;
                response.Messages.Add("One or more items exist in collection.");
            }

            await _repository.Delete(collectionId);
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
