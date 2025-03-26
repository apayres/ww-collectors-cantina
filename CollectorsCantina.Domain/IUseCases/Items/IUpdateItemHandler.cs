using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Items
{
    public interface IUpdateItemHandler
    {
        Task<ApplicationResponse<Item>> Update(Item item);
    }
}