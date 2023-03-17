using DAL_B3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using DAL_B3.DomainClass;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL_B3.Context;

namespace B3_C1.Controllers
{
    public class UsersStoredController : Controller
    {
        private readonly IUserStoredRepository _StoredRepo;
        private readonly BaiTap2_DbContext _context;

        public UsersStoredController(IUserStoredRepository repository, BaiTap2_DbContext context)
        {
            _StoredRepo = repository;
            _context = context;
        }


        [HttpGet]
        public IActionResult Index(string filter)
        {
            var data = _StoredRepo.GetAll();

            if (filter == "2")
            {
                data = _StoredRepo.GetAll().Where(c=>c.Status == true).ToList();
            }
            else if (filter == "1")
            {
                data = _StoredRepo.GetAll().Where(c => c.Status == false).ToList();
            }
            else
            {
                data = _StoredRepo.GetAll().ToList();
            }
            List<SelectListItem> isStatus = new List<SelectListItem>();

            isStatus.Add(new SelectListItem() { Text = "Tất cả", Value = "0" });
            isStatus.Add(new SelectListItem() { Text = "Đã xóa", Value = "1" });
            isStatus.Add(new SelectListItem() { Text = "Chưa xóa", Value = "2" });

            ViewData["isStatus"] = isStatus;

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleCode");
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Create(User cv)
        {
            if (ModelState.IsValid)
            {
                cv.Status = true;
                var userCreate = HttpContext.Session.GetString("UserName");
                cv.CreatedPerson = userCreate;
                _StoredRepo.Add(cv);
                return RedirectToAction("Index", "UsersStored");
            }

            return RedirectToAction("Create", "UsersStored");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var group = _StoredRepo.GetById(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, User cv)
        {
            if (id != cv.UserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _StoredRepo.Update(cv.UserId, cv.UserName, cv.Password, cv.RoleId, cv.Status, cv.CreatedPerson,cv.Description,cv.Phone);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _StoredRepo.Delete(id);
                return RedirectToAction("Index", "UsersStored");
            }
            catch
            {
                return RedirectToAction("Index", "UsersStored");
            }
        }
    }
}
