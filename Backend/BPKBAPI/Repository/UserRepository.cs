using BPKBAPI.Data;
using BPKBAPI.Models;
using BPKBAPI.Repository.Interface;

namespace BPKBAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password && u.IsActive == true);
        }
    }
}
