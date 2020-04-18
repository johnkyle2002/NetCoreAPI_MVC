using Microsoft.Extensions.Configuration;
using NetCoreMVC.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMVC.Commons.Helper
{
    public interface IClientHelper
    {
        Task<HttpResponseMessage> Authenticate(string uriPath, object param);
        Task<APIResponseViewModel<TModel>> GetClient<TModel>(string uriPath, string param, string token);
        Task<APIResponseViewModel<TModel>> PostClient<TModel>(string uriPath, TModel param, string token);
        Task<APIResponseViewModel<TModel>> PutClient<TModel>(string uriPath, TModel param, string token);
        Task<APIResponseViewModel<TModel>> DeleteClient<TModel>(string uriPath, string token);
    }

    public class ClientHelper : IClientHelper
    {
        private readonly string _uri;
        private readonly MediaTypeWithQualityHeaderValue _mediaType = new MediaTypeWithQualityHeaderValue("application/json");

        public ClientHelper(IConfiguration config)
        {
            _uri = config["jwt:uri"];
        }

        public async Task<APIResponseViewModel<TModel>> GetClient<TModel>(string uriPath, string param, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(_mediaType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(_uri + uriPath);

            HttpResponseMessage response;
            if (param != null)
            {
                response = await client.GetAsync(_uri + uriPath + param);
            }
            else
            {
                response = await client.GetAsync(uriPath);
            }

            if (response.IsSuccessStatusCode)
            {
                var reader = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<TModel>(reader);
                return new APIResponseViewModel<TModel>
                {
                    Entity = model,
                    RequestMessage = response.RequestMessage,
                    StatusCode = response.StatusCode
                };
            }
            return null;
        }

        public async Task<APIResponseViewModel<TModel>> PostClient<TModel>(string uriPath, TModel param, string token)
        {
            var jsonParam = JsonConvert.SerializeObject(param);
            var jsonData = new StringContent(jsonParam, Encoding.UTF8, _mediaType.MediaType);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(_mediaType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync(_uri + uriPath, jsonData);

                if (response.IsSuccessStatusCode)
                {
                    var model = await response.Content.ReadAsAsync<TModel>();
                    return new APIResponseViewModel<TModel>
                    {
                        Entity = model,
                        RequestMessage = response.RequestMessage,
                        StatusCode = response.StatusCode
                    };
                }
                return null;
            }
        }

        public async Task<APIResponseViewModel<TModel>> PutClient<TModel>(string uriPath, TModel param, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(_mediaType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PutAsJsonAsync<TModel>(_uri + uriPath, param);

                if (response.IsSuccessStatusCode)
                {
                    var model = await response.Content.ReadAsAsync<TModel>();
                    return new APIResponseViewModel<TModel>
                    {
                        Entity = model,
                        RequestMessage = response.RequestMessage,
                        StatusCode = response.StatusCode
                    };
                }
                return null;
            }
        }

        public async Task<APIResponseViewModel<TModel>> DeleteClient<TModel>(string uriPath, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(_mediaType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.BaseAddress = new Uri(_uri + uriPath);
                HttpResponseMessage response;

                try
                {
                    response = await client.DeleteAsync(new Uri(_uri + uriPath)); 
                    if (response.IsSuccessStatusCode)
                    {
                        var model = await response.Content.ReadAsAsync<TModel>();
                        return new APIResponseViewModel<TModel>
                        {
                            Entity = model,
                            RequestMessage = response.RequestMessage,
                            StatusCode = response.StatusCode
                        };
                    }
                }
                catch (Exception ex)
                {

                }
                return null;
            }
        }

        public async Task<HttpResponseMessage> Authenticate(string uriPath, object param)
        {
            var jsonParam = JsonConvert.SerializeObject(param);
            var jsonData = new StringContent(jsonParam, Encoding.UTF8, _mediaType.MediaType);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(_uri + uriPath, jsonData);
                return response;
            }
        }

    }
}
