using System.Threading.Tasks;
using BPKBAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BPKBAPI.Service.Interface
{
    public interface IStorageLocationService
    {
        Task<IEnumerable<StorageLocation>> GetAllStorageLocationsAsync();
    }
}
