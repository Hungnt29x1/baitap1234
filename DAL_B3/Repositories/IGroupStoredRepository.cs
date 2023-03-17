using DAL_B3.DomainClass;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public interface IGroupStoredRepository
    {
        void Add(Group group);
        void Update(Guid id, string groupCode, string description, bool status, DateTime fromDate, DateTime toDate);
        void Delete(Guid id);
        Group GetById(Guid id);
        List<Group> GetAll();
    }
}
