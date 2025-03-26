using CollectorsCantina.Web.Models.Items;

namespace CollectorsCantina.Web.Services.Items
{
    public interface IItemsService
    {
        Task<ItemsViewModel> LoadModel(Guid collectionId);
        Task<ItemsViewModel> LoadModel(string tag);
    }
}