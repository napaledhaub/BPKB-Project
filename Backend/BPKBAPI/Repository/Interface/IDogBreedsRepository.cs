using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IDogBreedsRepository
    {
        Task<DogBreedsResponse> FetchBreedsAsync();
    }
}
