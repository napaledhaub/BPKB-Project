using System.Net.Http;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPKBAPI.Repository
{
    public class DogBreedsRepository : IDogBreedsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public DogBreedsRepository(HttpClient httpClient, IOptions<DogApiSettings> options)
        {
            _httpClient = httpClient;
            _apiUrl = options.Value.GetDogBreedsUrl;
        }

        public async Task<DogBreedsResponse> FetchBreedsAsync()
        {
            var response = await _httpClient.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<DogBreedsResponse>(response);
        }
    }
}
