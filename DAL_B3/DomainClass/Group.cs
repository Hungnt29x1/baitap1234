using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.DomainClass
{
    public class Group
    {
        public Guid GroupId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã nhóm")]
        [StringLength(10, ErrorMessage = "Mã không được vượt quá 10 kí tự")]
        public string GroupCode { get; set; }

        public string Description { get; set; }


        public bool Status { get; set; }

        public string CreatedPerson { get; set; }
        public DateTime? FromDate { get; set;}
        public DateTime? ToDate { get; set;}



    }
}
