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

        private static readonly int MaxRetries = 3;
        private static readonly TimeSpan RetryInterval = TimeSpan.FromSeconds(2);

        public UserRepository()
        {
            //context = new EndoscopicEntities();
        }

        public User GetUserLogin(string userName)
        {
            User user = new User();

            var retryCount = 0;
            while (true)
            {
                try
                {
                    using (var context = new EndoscopicEntities())
                    {
                        var users = from o in context.Users
                                    where o.UserName == userName
                                    select o;
                        user = users.FirstOrDefault();
                    }
                    break;
                }
                catch (Exception)
                {
                    if (++retryCount >= MaxRetries)
                    {
                        throw;
                    }
                    Console.WriteLine("Retrying after {0} seconds...", RetryInterval.TotalSeconds);
                    System.Threading.Thread.Sleep(RetryInterval);
                }
            }
            
            return user;
        }
    }
}
