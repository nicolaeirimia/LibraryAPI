using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LibraryAPI.Contracts;
using LibraryAPI.Models;

namespace LibraryAPI.Classes
{
    public class MyApiClient : IMyApiClient
    {
        private readonly HttpClient _httpClient;

        public MyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResponseMessage> GetBookAsync(string name)
        {
            return await _httpClient.GetAsync($"?q={name}&fields=title,author_name&limit=1");
        }


    }
}
