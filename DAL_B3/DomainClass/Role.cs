using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.DomainClass
{
    public class Role
    {
        public Guid? RoleId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phân quyền")]
        [StringLength(10, ErrorMessage = "Mã không được vượt quá 10 kí tự")]
        public string RoleCode { get; set; }

        [Required(ErrorMessage = "Vui lòng mô tả phân quyền")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 kí tự")]
        public string RoleDescription { get; set; }

        public string Alias { get; set; }//...-..-..

        public bool Status { get; set; } = true;// Bằng true sẽ chưa bị xóa trên table

        public List<User> Users { get; set; }
    }
}
