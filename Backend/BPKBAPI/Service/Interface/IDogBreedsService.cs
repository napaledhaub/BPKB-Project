using BPKBAPI.Models;

namespace BPKBAPI.Service.Interface
{
    public interface IDogBreedsService
    {
        Task<DogBreedsResponse> FetchBreedsAsync();
    }
}
