using System.Collections.Generic;
using System.Linq;

namespace B1_c1.Models.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User> {
        new User { Id = 1, UserName = "admin", Password = "admin123" },
        new User { Id = 2, UserName = "hoa", Password = "bluerose" },
        new User { Id = 3, UserName = "nhan", Password = "nhannha95" },

    };

        public User GetUser(string userName, string password)
        {
            return _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

    }
}
