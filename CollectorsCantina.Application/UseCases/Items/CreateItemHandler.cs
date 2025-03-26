using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IUseCases.Items;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Items
{
    public class CreateItemHandler : ICreateItemHandler
    {
        private readonly IItemRepository _repository;

        public CreateItemHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResponse<Item>> Create(Item item)
        {
            var response = new ApplicationResponse<Item>();
            response.Data = item;

            var validator = new ItemValidator();
            if (!validator.ValidateItem(item))
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = validator.Errors;
                return response;
            }

            response.Data = await _repository.Insert(item);
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
