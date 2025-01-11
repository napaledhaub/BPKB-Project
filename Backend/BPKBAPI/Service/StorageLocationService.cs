using System.Security.Cryptography.X509Certificates;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using BPKBAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BPKBAPI.Service
{
    public class StorageLocationService : IStorageLocationService
    {
        private readonly IStorageLocationRepository _repository;

        public StorageLocationService(IStorageLocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StorageLocation>> GetAllStorageLocationsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
