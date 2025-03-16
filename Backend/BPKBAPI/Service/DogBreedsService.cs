using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using BPKBAPI.Service.Interface;
using Newtonsoft.Json;

namespace BPKBAPI.Service
{
    public class DogBreedsService : IDogBreedsService
    {
        private readonly IDogBreedsRepository _repository;

        public DogBreedsService(IDogBreedsRepository repository)
        {
            _repository = repository;
        }

        public async Task<DogBreedsResponse> FetchBreeds()
        {
            return await _repository.FetchBreeds();
        }

        public async Task<DogImageResponse> GetDogImages(string breed, int numberOfImages)
        {
            DogBreedsResponse dogBreedsResponse = await _repository.FetchBreeds();
            Dictionary<string, List<string>> dogBreeds = dogBreedsResponse.Message;
            if (!dogBreeds.ContainsKey(breed))
            {
                return new DogImageResponse { Status = "failed", Message = null};
            }

            return await _repository.GetDogImages(breed, numberOfImages);
        }
    }
}
