using System.ComponentModel.DataAnnotations;

namespace B3_C1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Username không được vượt quá 100 kí tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trông")]
        [StringLength(20, ErrorMessage = "Nhập ít nhất từ 8 -> 20 kí tự", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
