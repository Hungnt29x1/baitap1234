using DAL_B3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public interface ISMSRepository
    {
        bool Add(SMS obj);

        bool Update(SMS obj);

        bool Delete(SMS obj);
        SMS GetById(Guid id);

        List<SMS> GetAll();
    }
}
