using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IUseCases.Items;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Items
{
    public class GetItemHandler : IGetItemHandler
    {
        private readonly IItemRepository _repository;

        public GetItemHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResponse<Item>> Get(Guid itemId)
        {
            var response = new ApplicationResponse<Item>();
            response.Data = await _repository.Get(itemId);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ApplicationResponse<List<Item>>> GetByCollection(Guid collectionId)
        {
            var response = new ApplicationResponse<List<Item>>();
            response.Data = await _repository.GetByCollection(collectionId);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<ApplicationResponse<List<Item>>> GetByTerm(string term)
        {
            var response = new ApplicationResponse<List<Item>>();
            var allItems = await _repository.GetAll();
            term = term.ToLower();

            response.Data = allItems.Where(x => x.Name.ToLower().Contains(term) || x.Tags != null && x.Tags.Any(t => t.ToLower() == term)).ToList();
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
