using BPKBAPI.Models;

namespace BPKBAPI.Service.Interface
{
    public interface IDogBreedsService
    {
        Task<DogBreedsResponse> FetchBreeds();
        Task<DogImageResponse> GetDogImages(string breed, int numberOfImages);
    }
}
