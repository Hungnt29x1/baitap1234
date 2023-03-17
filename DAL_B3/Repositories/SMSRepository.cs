using DAL_B3.Context;
using DAL_B3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public class SMSRepository:ISMSRepository
    {
        private readonly BaiTap2_DbContext _context;

        public SMSRepository(BaiTap2_DbContext context)
        {
            _context = context;
        }

        public bool Add(SMS obj)
        {
            if (obj == null) return false;
            _context.SMSs.Add(obj);
            //_context.Stores.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(SMS obj)
        {
            if (obj == null) return false;
            _context.SMSs.Remove(obj);
            _context.SaveChanges();
            return true;
        }


        public List<SMS> GetAll()
        {
            return _context.SMSs.OrderByDescending(c => c.CreateSend).ToList();
        }

        public SMS GetById(Guid id)
        {
            return _context.SMSs.Where(c => c.SmsId == id).FirstOrDefault();
        }

        public bool Update(SMS obj)
        {
            if (obj == null) return false;
            _context.SMSs.Update(obj);
            _context.SaveChanges();
            return true;
        }
    }
}
