using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Collections
{
    public interface ICreateCollectionHandler
    {
        Task<ApplicationResponse<Collection>> Create(Collection collection);
    }
}