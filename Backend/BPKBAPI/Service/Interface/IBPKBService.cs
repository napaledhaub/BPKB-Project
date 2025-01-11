using BPKBAPI.Models;

namespace BPKBAPI.Service.Interface
{
    public interface IBPKBService
    {
        Task CreateBPKBAsync(BPKB bpkb);
    }
}
