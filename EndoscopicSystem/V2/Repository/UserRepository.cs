using EndoscopicSystem.V2.Database;
using System.Linq;

namespace EndoscopicSystem.V2.Repository
{
    public interface IUserRepository
    {
        User GetUserLogin(string userName);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly Database1Entities _context;

        public UserRepository()
        {
            _context = new Database1Entities();
        }

        public User GetUserLogin(string userName)
        {
            var users = from o in _context.Users
                        where o.UserName == userName
                        select o;
            User user = users.FirstOrDefault();
            return user;
        }
    }
}
