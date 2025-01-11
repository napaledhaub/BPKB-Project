using BPKBAPI.Repository.Interface;
using BPKBAPI.Service.Interface;

namespace BPKBAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ValidateUser(string username, string password)
        {
            var user = _userRepository.GetUser(username, password);
            return user != null;
        }
    }
}
