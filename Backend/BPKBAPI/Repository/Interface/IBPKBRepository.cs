using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IBPKBRepository
    {
        Task AddBPKBAsync(BPKB bpkb);
        Task<BPKB> GetByAgreementNumberAsync(string agreementNumber);
        Task UpdateBPKBAsync(BPKB bpkb);
        Task DeleteBPKBAsync(string agreementNumber);
    }
}
