using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.DomainClass
{
    public class SMS
    {
        public Guid SmsId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        public string Phone { get; set; }

        public DateTime? CreateSend { get; set; }

        
        public string CreatedPerson { get; set; }
    }
}
