using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IBPKBRepository
    {
        Task AddBPKBAsync(BPKB bpkb);
        Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetByAgreementNumberAsync(string agreementNumber, int pageNumber, int pageSize);
        Task UpdateBPKBAsync(BPKB bpkb);
        Task DeleteBPKBAsync(string agreementNumber);
        Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetBPKBsAsync(int pageNumber, int pageSize);
    }
}
