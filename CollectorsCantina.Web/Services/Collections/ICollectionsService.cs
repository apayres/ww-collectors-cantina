using CollectorsCantina.Web.Models.Collections;

namespace CollectorsCantina.Web.Services.Collections
{
    public interface ICollectionsService
    {
        Task<CollectionsViewModel> LoadModel();
        Task<CollectionsViewModel> LoadFavorites();
    }
}