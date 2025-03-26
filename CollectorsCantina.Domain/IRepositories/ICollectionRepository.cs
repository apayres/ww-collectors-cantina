using CollectorsCantina.Domain.Entities;

namespace CollectorsCantina.Domain.IRepositories
{
    public interface ICollectionRepository
    {
        Task<List<Collection>> GetAll();

        Task<Collection> Get(Guid id);

        Task<Collection> Insert(Collection collection);

        Task<Collection> Update(Collection collection);

        Task Delete(Guid collectionId);
    }
}
