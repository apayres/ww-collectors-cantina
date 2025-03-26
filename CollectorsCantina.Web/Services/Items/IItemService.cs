using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Items;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Services.Items
{
    public interface IItemService
    {
        Task<ItemViewModel> LoadModel(Guid? id);
        Task<ItemViewModel> ProcessModel(ItemViewModel model, ProcessModelAction action);
        Task<string> UploadImage(IBrowserFile file, string name, string container);
    }
}