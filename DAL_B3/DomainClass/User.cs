using System;
using System.ComponentModel.DataAnnotations;

namespace DAL_B3.DomainClass
{
    public class User
    {
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập UserName")]
        [StringLength(10, ErrorMessage = "UserName không được vượt quá 10 kí tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(20, ErrorMessage = "Nhập ít nhất từ 8 -> 20 kí tự", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "Nhập đúng định dạng 1 Hoa - thường - số - kí tự")]// Check 1 số
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public Role RoleId_Navigation { get; set; }
        public bool Status { get; set; } = true;// True là còn hoạt động chưa bị xóa

        public string? Description { get; set; }
        public string? CreatedPerson { get; set; }
        public string? Phone { get; set; }

    }
}