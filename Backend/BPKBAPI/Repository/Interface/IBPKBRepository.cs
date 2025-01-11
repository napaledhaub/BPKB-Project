using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IBPKBRepository
    {
        Task AddBPKBAsync(BPKB bpkb);
    }
}
