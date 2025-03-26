using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Items
{
    public interface ICreateItemHandler
    {
        Task<ApplicationResponse<Item>> Create(Item item);
    }
}