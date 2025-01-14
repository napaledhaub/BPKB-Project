using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;
using BPKBAPI.Service.Interface;

namespace BPKBAPI.Service
{
    public class BPKBService : IBPKBService
    {
        private readonly IBPKBRepository _repository;

        public BPKBService(IBPKBRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateBPKBAsync(BPKB bpkb)
        {
            bpkb.CreatedOn = DateTime.UtcNow;
            await _repository.AddBPKBAsync(bpkb);
        }

        public async Task<BPKB> GetBPKBByAgreementNumberAsync(string agreementNumber)
        {
            return await _repository.GetByAgreementNumberAsync(agreementNumber);
        }

        public async Task UpdateBPKBAsync(BPKB bpkb)
        {
            bpkb.LastUpdateOn = DateTime.UtcNow;
            await _repository.UpdateBPKBAsync(bpkb);
        }

        public async Task DeleteBPKBAsync(string agreementNumber)
        {
            await _repository.DeleteBPKBAsync(agreementNumber);
        }

    }
}
