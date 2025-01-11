using BPKBAPI.Data;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;

namespace BPKBAPI.Repository
{
    public class BPKBRepository : IBPKBRepository
    {
        private readonly ApplicationDbContext _context;

        public BPKBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBPKBAsync(BPKB bpkb)
        {
            await _context.BPKBs.AddAsync(bpkb);
            await _context.SaveChangesAsync();
        }
    }
}
