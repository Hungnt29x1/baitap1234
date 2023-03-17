using DAL_B3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public interface IGroupRepository
    {
        bool Add(Group obj);

        bool Update(Group obj);

        bool Delete(Group obj);
        Group GetById(Guid id);

        List<Group> GetByName(string name);

        List<Group> GetAll();

    }
}
