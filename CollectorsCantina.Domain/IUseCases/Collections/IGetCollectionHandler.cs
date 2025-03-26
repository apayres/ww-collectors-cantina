using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Collections
{
    public interface IGetCollectionHandler
    {
        Task<ApplicationResponse<List<Collection>>> GetAll();
        Task<ApplicationResponse<Collection>> Get(Guid id);
    }
}