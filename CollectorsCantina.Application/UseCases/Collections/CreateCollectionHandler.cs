using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IUseCases.Collections;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Collections
{
    public class CreateCollectionHandler : ICreateCollectionHandler
    {
        private readonly ICollectionRepository _repository;

        public CreateCollectionHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResponse<Collection>> Create(Collection collection)
        {
            var response = new ApplicationResponse<Collection>();
            response.Data = collection;

            var validator = new CollectionValidator();
            if (!validator.ValidateCollection(collection))
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = validator.Errors;
                return response;
            }

            response.Data = await _repository.Insert(collection);
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
