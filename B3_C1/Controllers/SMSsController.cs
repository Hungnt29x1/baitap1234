using B3_C1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using DAL_B3.DomainClass;
using System.Linq;
using DAL_B3.Context;

namespace B3_C1.Controllers
{
    public class SMSsController : Controller
    {
        private readonly ISMSService _Service;
        private readonly BaiTap2_DbContext _context;

        public SMSsController(ISMSService service, BaiTap2_DbContext context)
        {
            _Service = service;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string keyword, DateTime tuNgay, DateTime denNgay, string filter)
        {
            var data = _Service.GetAll();

            if (keyword != null && tuNgay != default(DateTime) && denNgay != default(DateTime) && filter != null && filter != "0")
            {
                data = data.Where(c => c.Body.Contains(keyword) || c.CreateSend >= tuNgay || c.CreateSend <= denNgay || c.CreatedPerson == filter).ToList();
            }
            else if (keyword != null)
            {
                data = data.Where(c => c.Body.Contains(keyword)).ToList();
            }
            else if (filter != null && filter != "0")
            {
                data = data.Where(c => c.CreatedPerson == filter).ToList();
            }
            else if (tuNgay != default(DateTime) && denNgay != default(DateTime))
            {
                data = data.Where(c => c.CreateSend >= tuNgay && c.CreateSend <= denNgay).ToList();
            }
            else if (tuNgay != default(DateTime))
            {
                data = data.Where(c => c.CreateSend >= tuNgay).ToList();
            }
            else
            {
                data = data.ToList();
            }
            ViewData["UserID"] = new SelectList(_context.Users.ToList(), "UserName", "UserName");

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Create(SMS cv)
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                if (ModelState.IsValid)
                {
                    cv.CreatedPerson = userCreate;
                    cv.CreateSend = DateTime.Now;
                    _Service.Create(cv);
                    return RedirectToAction("Index", "SMSs");
                }
            }
            return RedirectToAction("Login", "Login");

        }


        [HttpGet]
        public IActionResult Delete()
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _Service.Delete(id);
                return RedirectToAction("Index", "SMSs");
            }
            catch
            {
                return RedirectToAction("Index", "SMSs");
            }
        }
    }
}
