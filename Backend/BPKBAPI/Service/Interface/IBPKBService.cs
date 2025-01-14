using BPKBAPI.Models;

namespace BPKBAPI.Service.Interface
{
    public interface IBPKBService
    {
        Task CreateBPKBAsync(BPKB bpkb);
        Task<BPKB> GetBPKBByAgreementNumberAsync(string agreementNumber);
        Task UpdateBPKBAsync(BPKB bpkb);
        Task DeleteBPKBAsync(string agreementNumber);
    }
}
