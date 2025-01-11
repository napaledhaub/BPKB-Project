namespace BPKBAPI.Service.Interface
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);
    }
}
