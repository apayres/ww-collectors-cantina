using CollectorsCantina.Domain.Entities;

namespace CollectorsCantina.Domain.IRepositories
{
    public interface IItemRepository
    {
        Task Delete(Guid itemId, Guid collectionId);
        Task<Item> Get(Guid id);
        Task<List<Item>> GetByCollection(Guid id);
        Task<List<Item>> GetAll();
        Task<Item> Insert(Item item);
        Task<Item> Update(Item item);
    }
}