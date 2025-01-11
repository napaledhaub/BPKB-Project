using BPKBAPI.Data;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BPKBAPI.Repository
{
    public class StorageLocationRepository : IStorageLocationRepository
    {
        private readonly ApplicationDbContext _context;

        public StorageLocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StorageLocation>> GetAllAsync()
        {
            return await _context.StorageLocations.ToListAsync();
        }
    }
}
