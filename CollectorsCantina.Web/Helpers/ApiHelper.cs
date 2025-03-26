using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Hosting;

namespace CollectorsCantina.Web.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiHelper> _logger;

        public ApiHelper(IHttpClientFactory httpClientFactory, ILogger<ApiHelper> logger)
        {
            _httpClient = httpClientFactory.CreateClient("CollectorsCantinaApi");
            _logger = logger;
        }

        public async Task<T?> Get<T>(string endPoint)
        {
            var response = await _httpClient.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<T>();
            return data;
        }

        public async Task<T?> Get<T>(string endPoint, object id)
        {
            return await Get<T>(endPoint +  "/" + id);
        }

        public async Task<T?> Get<T>(string endPoint, Dictionary<string, object> parameters)
        {
            var joinedParameters = GetParamsFromDictionary(parameters);
            return await Get<T>(endPoint + joinedParameters);
        }

        public async Task<T?> Put<T>(string endPoint, T data)
        {
            var put = await _httpClient.PutAsJsonAsync(endPoint, data);
            put.EnsureSuccessStatusCode();

            var response = await put.Content.ReadFromJsonAsync<T>();
            return response;
        }

        public async Task<T?> Post<T>(string endPoint, T data)
        {
            var post = await _httpClient.PostAsJsonAsync(endPoint, data);
            post.EnsureSuccessStatusCode();

            var response = await post.Content.ReadFromJsonAsync<T>();
            return response;
        }

        public async Task<T?> Upload<T>(string endPoint, IBrowserFile browserFile, string fileName, string container)
        {
            using var formContent = new MultipartFormDataContent();
            formContent.Headers.ContentType.MediaType = "multipart/form-data";
            
            using var stream = browserFile.OpenReadStream();
            formContent.Add(new StreamContent(stream), "file", fileName);

            var post = await _httpClient.PostAsync(endPoint + $"?container={container}&name={fileName}", formContent);
            post.EnsureSuccessStatusCode();

            var response = await post.Content.ReadFromJsonAsync<T>();
            return response;
        }

        public async Task Delete(string endPoint)
        {
            var delete = await _httpClient.DeleteAsync(endPoint);
            delete.EnsureSuccessStatusCode();
        }

        public async Task Delete(string endPoint, object id)
        {
            await Delete(endPoint + "/" + id);
        }

        public async Task Delete(string endPoint, Dictionary<string, object> parameters)
        {
            var joinedParameters = GetParamsFromDictionary(parameters);
            await Delete(endPoint + "?" + joinedParameters);
        }

        private static string GetParamsFromDictionary(Dictionary<string, object> parameters)
        {
            var pairs = parameters.Select(x => $"{x.Key}={x.Value}");
            return string.Join("&", pairs);
        }
    }
}
