using BPKBAPI.Models;

namespace BPKBAPI.Repository.Interface
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
    }
}
