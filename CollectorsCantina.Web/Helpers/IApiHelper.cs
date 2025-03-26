
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Helpers
{
    public interface IApiHelper
    {
        Task Delete(string endPoint);
        Task Delete(string endPoint, object id);
        Task Delete(string endPoint, Dictionary<string, object> parameters);
        Task<T?> Get<T>(string endPoint);
        Task<T?> Get<T>(string endPoint, object id);
        Task<T?> Get<T>(string endPoint, Dictionary<string, object> parameters);
        Task<T?> Post<T>(string endPoint, T data);
        Task<T?> Put<T>(string endPoint, T data);
        Task<T?> Upload<T>(string endPoint, IBrowserFile browserFile, string fileName, string container);
    }
}