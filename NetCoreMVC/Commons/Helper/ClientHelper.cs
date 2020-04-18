using Microsoft.Extensions.Configuration;
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
        Task<HttpResponseMessage> GetClient(string uriPath, string token);
        Task<HttpResponseMessage> PostClient(string uriPath, object param);
    }

    public class ClientHelper : IClientHelper
    {
        private readonly string _uri;
        private readonly MediaTypeWithQualityHeaderValue _mediaType = new MediaTypeWithQualityHeaderValue("application/json");

        public ClientHelper(IConfiguration config)
        {
            _uri = config["jwt:uri"];
        }

        public async Task<HttpResponseMessage> GetClient(string uriPath, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(_mediaType);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            client.BaseAddress = new Uri(_uri + uriPath);
            return await client.GetAsync(uriPath);
        }

        public async Task<HttpResponseMessage> PostClient(string uriPath, object param)
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
