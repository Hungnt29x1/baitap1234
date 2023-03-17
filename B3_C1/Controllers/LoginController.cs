using B3_C1.Models;
using DAL_B3.Context;
using DAL_B3.DomainClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace B3_C1.Controllers
{
    public class LoginController : Controller
    {
        private readonly BaiTap2_DbContext _context;

        public LoginController(BaiTap2_DbContext context)
        {
            _context = context;   
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        [Authorize(Roles = "AD")] // xác thực người dùng ngay từ đầu
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanId = HttpContext.Session.GetString("UserId");// Khi vào session sẽ check Id của m có chưa (nếu chưa có thì mới cho nó vào view
            var taikhoanName = HttpContext.Session.GetString("UserName");// Khi vào session sẽ check Id của m có chưa (nếu chưa có thì mới cho nó vào view
            if (taikhoanId != null) return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        [Authorize(Roles = "AD")] // xác thực người dùng ngay từ đầu
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    User kh = _context.Users.Include(p => p.RoleId_Navigation)
                        .SingleOrDefault(p => p.UserName.ToLower() == model.Username.ToLower().Trim() && p.Status == true);

                    if (kh == null)
                    {
                        ViewBag.Error = " Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    string pass = (model.Password.Trim());
                    if (kh.Password.Trim() != pass)
                    {
                        ViewBag.Error = " Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    // Đăng nhập thành công


                    //var taikhoanID = HttpContext.Session.GetString("UserId");

                    //Identity
                    //Lưu Session Makh
                    HttpContext.Session.SetString("UserId", kh.UserId.ToString());
                    HttpContext.Session.SetString("UserName", kh.UserName.ToString());


                    //Identity
                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,kh.UserName),
                        new Claim("UserId",kh.UserId.ToString()),
                        new Claim("RoleId",kh.RoleId.ToString()),
                        new Claim(ClaimTypes.Role,kh.RoleId_Navigation.RoleCode)
                    };

                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[]
                    {
                        grandmaIdentity
                    });
                    await HttpContext.SignInAsync(userPrincipal);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {

                    return RedirectToAction("Login", "Login");
                }
            }

            return View();
        }

        [Route("dang-xuat.html", Name = "Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("UserId");
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
