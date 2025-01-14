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

        public async Task<BPKB> GetByAgreementNumberAsync(string agreementNumber)
        {
            return await _context.BPKBs.FindAsync(agreementNumber);
        }

        public async Task UpdateBPKBAsync(BPKB bpkb)
        {
            _context.BPKBs.Update(bpkb);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBPKBAsync(string agreementNumber)
        {
            var bpkb = await _context.BPKBs.FindAsync(agreementNumber);
            if (bpkb != null)
            {
                _context.BPKBs.Remove(bpkb);
                await _context.SaveChangesAsync();
            }
        }

    }
}
