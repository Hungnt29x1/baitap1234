using System.Collections.Generic;
using System;
using DAL_B3.Repositories;
using DAL_B3.DomainClass;

namespace B3_C1.Models
{
    public class GroupService:IGroupService
    {
        private IGroupRepository _iRepo;

        public GroupService(IGroupRepository iRepo)
        {
            _iRepo = iRepo;
        }
        public bool Create(Group cv)
        {
            if (cv == null) return false;
            if (_iRepo.Add(cv))
            {
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            if (id == null) return false;
            var result = _iRepo.GetById(id);
            if (_iRepo.Delete(result))
            {
                return true;
            }
            return false;
        }

        public List<Group> GetByName(string name)
        {
            return _iRepo.GetByName(name);
        }

        public List<Group> GetAll()
        {
            return _iRepo.GetAll();
        }

        public Group GetById(Guid id)
        {
            return _iRepo.GetById(id);
        }

        public bool Update(Guid id, Group cv)
        {
            if (id == null) return false;
            var result = _iRepo.GetById(id);
            result.GroupCode = cv.GroupCode;
            result.Description = cv.Description;
            result.FromDate = cv.FromDate;
            result.ToDate = cv.ToDate;
            result.Status = cv.Status;
            if (_iRepo.Update(result))
            {
                return true;
            }
            return false;
        }
    }
}
