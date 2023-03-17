using B1_c1.Models.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B1_c1.Controllers
{
    public class AccountController : Controller
    {
        public IUserService _userService;

        public AccountController()
        {
            _userService = new UserService();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không được để trống.";
                return View();
            }

            var user = _userService.GetUser(userName, password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu. Đề nghị nhập lại.";
                return View();
            }

            // Lưu thông tin người dùng vào Session để đánh dấu đã đăng nhập thành công
            //Session["UserId"] = user.Id;
            //Session["UserName"] = user.UserName;
            HttpContext.Session.SetString("Id", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserName", userName);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            //Session.Clear(); // Xóa thông tin người dùng đã lưu trong Session
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("Id");
            return RedirectToAction("Login");
        }
    }
}
