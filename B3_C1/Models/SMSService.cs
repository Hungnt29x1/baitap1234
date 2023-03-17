using DAL_B3.Repositories;
using System.Collections.Generic;
using System;
using DAL_B3.DomainClass;

namespace B3_C1.Models
{
    public class SMSService : ISMSService
    {
        private ISMSRepository _iRepo;

        public SMSService(ISMSRepository iRepo)
        {
            _iRepo = iRepo;
        }
        public bool Create(SMS cv)
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


        public List<SMS> GetAll()
        {
            return _iRepo.GetAll();
        }

        public SMS GetById(Guid id)
        {
            return _iRepo.GetById(id);
        }

    }
}
