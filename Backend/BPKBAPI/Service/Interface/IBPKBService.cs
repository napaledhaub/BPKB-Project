using BPKBAPI.Models;

namespace BPKBAPI.Service.Interface
{
    public interface IBPKBService
    {
        Task CreateBPKBAsync(BPKB bpkb);
        Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetBPKBByAgreementNumberAsync(string agreementNumber, int pageNumber, int pageSize);
        Task UpdateBPKBAsync(BPKB bpkb);
        Task DeleteBPKBAsync(string agreementNumber);
        Task<(List<BPKBWithLocation> BPKBs, int TotalCount)> GetBPKBsAsync(int pageNumber, int pageSize);
    }
}
