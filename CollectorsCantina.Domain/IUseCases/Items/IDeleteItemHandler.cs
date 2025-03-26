using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Items
{
    public interface IDeleteItemHandler
    {
        Task<ApplicationResponse<Item>> Delete(Guid itemId, Guid collectionId);
    }
}