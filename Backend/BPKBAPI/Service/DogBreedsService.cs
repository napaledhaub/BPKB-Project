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

        public async Task<DogBreedsResponse> FetchBreedsAsync()
        {
            return await _repository.FetchBreedsAsync();
        }
    }
}
