using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Collections
{
    public interface IUpdateCollectionHandler
    {
        Task<ApplicationResponse<Collection>> Update(Collection collection);
    }
}