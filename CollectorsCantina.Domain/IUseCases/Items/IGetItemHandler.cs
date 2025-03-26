using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Items
{
    public interface IGetItemHandler
    {
        Task<ApplicationResponse<Item>> Get(Guid itemId);
        Task<ApplicationResponse<List<Item>>> GetByCollection(Guid collectionId);
        Task<ApplicationResponse<List<Item>>> GetByTerm(string tag);
    }
}