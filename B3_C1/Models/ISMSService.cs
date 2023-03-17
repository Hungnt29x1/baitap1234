using System.Collections.Generic;
using System;
using DAL_B3.DomainClass;

namespace B3_C1.Models
{
    public interface ISMSService
    {
        public bool Create(SMS cv);

        public bool Delete(Guid id);

        public SMS GetById(Guid id);

        public List<SMS> GetAll();
    }
}
