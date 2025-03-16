using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IDogBreedsRepository
    {
        Task<DogBreedsResponse> FetchBreeds();
        Task<DogImageResponse> GetDogImages(string breed, int numberOfImages);
    }
}
