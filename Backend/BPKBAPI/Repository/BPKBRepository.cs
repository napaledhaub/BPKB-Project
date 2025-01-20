using BPKBAPI.Data;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetByAgreementNumberAsync(string agreementNumber, int pageNumber, int pageSize)
        {
            var query = from b in _context.BPKBs
                        join s in _context.StorageLocations on b.LocationID equals s.LocationID into joined
                        from s in joined.DefaultIfEmpty()
                        where EF.Functions.Like(b.AgreementNumber, $"%{agreementNumber}%")
                        select new BPKBWithLocation
                        {
                            AgreementNumber = b.AgreementNumber,
                            BPKBNo = b.BPKBNo,
                            BranchID = b.BranchID,
                            BPKBDate = b.BPKBDate,
                            FakturNo = b.FakturNo,
                            FakturDate = b.FakturDate,
                            LocationID = b.LocationID,
                            PoliceNo = b.PoliceNo,
                            BPKBDateIn = b.BPKBDateIn,
                            CreatedBy = b.CreatedBy,
                            CreatedOn = b.CreatedOn,
                            LastUpdateBy = b.LastUpdateBy,
                            LastUpdateOn = b.LastUpdateOn,
                            LocationName = s.LocationName
                        };

            var totalCount = await query.CountAsync();
            var bpkbs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bpkbs, totalCount);
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

        public async Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetBPKBsAsync(int pageNumber, int pageSize)
        {
            var query = from b in _context.BPKBs
                        join s in _context.StorageLocations on b.LocationID equals s.LocationID into joined
                        from s in joined.DefaultIfEmpty()
                        select new BPKBWithLocation
                        {
                            AgreementNumber = b.AgreementNumber,
                            BPKBNo = b.BPKBNo,
                            BranchID = b.BranchID,
                            BPKBDate = b.BPKBDate,
                            FakturNo = b.FakturNo,
                            FakturDate = b.FakturDate,
                            LocationID = b.LocationID,
                            PoliceNo = b.PoliceNo,
                            BPKBDateIn = b.BPKBDateIn,
                            CreatedBy = b.CreatedBy,
                            CreatedOn = b.CreatedOn,
                            LastUpdateBy = b.LastUpdateBy,
                            LastUpdateOn = b.LastUpdateOn,
                            LocationName = s.LocationName
                        };

            var totalCount = await query.CountAsync();
            var bpkbs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bpkbs, totalCount);
        }
    }
}
