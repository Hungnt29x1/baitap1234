using DAL_B3.Context;
using DAL_B3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public class GroupRepository: IGroupRepository
    {
        private readonly BaiTap2_DbContext _context;

        public GroupRepository(BaiTap2_DbContext context)
        {
            _context = context;
        }

        public bool Add(Group obj)
        {
            if (obj == null) return false;
            _context.Groups.Add(obj);
            //_context.Stores.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Group obj)
        {
            if (obj == null) return false;
            obj.Status = false;
            _context.Groups.Update(obj);
            _context.SaveChanges();
            return true;
        }

        public List<Group> GetByName(string name)
        {
            var query = from x in _context.Groups
                        .Where(c => c.Status == true)
                        select x;
            if (!String.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                query = query.Where(b => b.GroupCode.ToLower().Trim().Contains(name));
            }
            return query.ToList();
        }

        public List<Group> GetAll()
        {
            return _context.Groups.OrderByDescending(c => c.GroupCode).ToList();
        }

        public Group GetById(Guid id)
        {
            return _context.Groups.Where(c => c.GroupId == id).FirstOrDefault();
        }

        public bool Update(Group obj)
        {
            if (obj == null) return false;
            _context.Groups.Update(obj);
            _context.SaveChanges();
            return true;
        }
    }
}
