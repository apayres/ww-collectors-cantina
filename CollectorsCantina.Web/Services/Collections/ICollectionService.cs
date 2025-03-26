using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Collections;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Services.Collections
{
    public interface ICollectionService
    {
        Task<CollectionViewModel> LoadModel(Guid? id);
        Task<CollectionViewModel> ProcessModel(CollectionViewModel model, ProcessModelAction action);
        Task<string> UploadImage(IBrowserFile file, string name, string container);
    }
}