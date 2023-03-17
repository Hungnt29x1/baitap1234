using System.Collections.Generic;
using System;
using DAL_B3.DomainClass;

namespace B3_C1.Models
{
    public interface IGroupService
    {
        public bool Create(Group cv);

        public bool Update(Guid id, Group cv);

        public bool Delete(Guid id);

        public Group GetById(Guid id);
        public List<Group> GetByName(string name);

        public List<Group> GetAll();
    }
}
