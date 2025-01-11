using System.Security.Cryptography.X509Certificates;
using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IStorageLocationRepository
    {
        Task<IEnumerable<StorageLocation>> GetAllAsync();
    }
}
