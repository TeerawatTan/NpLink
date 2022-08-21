using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using System;
using System.Linq;

namespace EndoscopicSystem.Repository
{
    public interface IUserRepository
    {
        User GetUserLogin(string userName);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly EndoscopicEntities context;

        public UserRepository()
        {
            context = new EndoscopicEntities();
        }

        public User GetUserLogin(string userName)
        {
            var users = from o in context.Users
                        where o.UserName == userName
                        select o;
            User user = users.FirstOrDefault();
            return user;
        }
    }
}
