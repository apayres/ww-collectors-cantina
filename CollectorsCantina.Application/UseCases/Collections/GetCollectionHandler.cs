using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IUseCases.Collections;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Collections
{
    public class GetCollectionHandler : IGetCollectionHandler
    {
        private readonly ICollectionRepository _repository;

        public GetCollectionHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResponse<Collection>> Get(Guid id)
        {
            var response = new ApplicationResponse<Collection>();
            response.Data = await _repository.Get(id);
            response.Status = ResponseStatus.Success;
            return response;
        }


        public async Task<ApplicationResponse<List<Collection>>> GetAll()
        {
            var response = new ApplicationResponse<List<Collection>>();
            response.Data = await _repository.GetAll();
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
