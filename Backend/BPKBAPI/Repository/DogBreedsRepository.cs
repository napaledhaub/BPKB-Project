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
        private readonly string _getDogBreedsUrl;
        private readonly string _getDogImagesUrl;

        public DogBreedsRepository(HttpClient httpClient, IOptions<DogApiSettings> options)
        {
            _httpClient = httpClient;
            _getDogBreedsUrl = options.Value.GetDogBreedsUrl;
            _getDogImagesUrl = options.Value.GetDogImageUrl;
        }

        public async Task<DogBreedsResponse> FetchBreeds()
        {
            var response = await _httpClient.GetStringAsync(_getDogBreedsUrl);
            return JsonConvert.DeserializeObject<DogBreedsResponse>(response);
        }

        public async Task<DogImageResponse> GetDogImages(string breed, int numberOfImages)
        {
            string url = _getDogImagesUrl + breed + "/images/random/" + numberOfImages;
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<DogImageResponse>(response);
        }
    }
}
