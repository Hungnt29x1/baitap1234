using DAL_B3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public interface IUserStoredRepository
    {
        void Add(User group);
        void Update(Guid? id, string userName, string password, Guid? roleID, bool status, string createPerson, string description, string phone);
        void Delete(Guid id);
        User GetById(Guid id);
        List<User> GetAll();
    }
}
