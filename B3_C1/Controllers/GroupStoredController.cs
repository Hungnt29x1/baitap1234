using B3_C1.Models;
using DAL_B3.DomainClass;
using DAL_B3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace B3_C1.Controllers
{
    public class GroupStoredController : Controller
    {
        private readonly IGroupStoredRepository _groupStoredRepo;

        public GroupStoredController(IGroupStoredRepository groupStoredRepo)
        {
            _groupStoredRepo = groupStoredRepo;
        }


        [HttpGet]
        public IActionResult Index(string keyword, string filter)
        {
            var data = _groupStoredRepo.GetAll();
            if (keyword != null)
            {
                data = data.Where(c => c.GroupCode == keyword || c.CreatedPerson == keyword).ToList();
            }

            if (filter == "0")
            {
                data = data.Where(c => c.Status == true).ToList();
            }
            else if (filter == "1")
            {
                data = data.Where(c => c.Status == false).ToList();
            }
            else
            {
                data = data.Where(c => c.Status == true).ToList();
            }

            List<SelectListItem> isStatus = new List<SelectListItem>();

            isStatus.Add(new SelectListItem() { Text = "Chưa xóa", Value = "0" });
            isStatus.Add(new SelectListItem() { Text = "Đã xóa", Value = "1" });


            ViewData["isStatus"] = isStatus;
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
        public IActionResult Create(Group cv)
        {
            if (ModelState.IsValid)
            {
                cv.Status = true;
                var userCreate = HttpContext.Session.GetString("UserName");
                cv.CreatedPerson = userCreate;
                _groupStoredRepo.Add(cv);
                return RedirectToAction("Index", "GroupStored");
            }

            return RedirectToAction("Create", "GroupStored");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                var group = _groupStoredRepo.GetById(id);

                if (group == null)
                {
                    return NotFound();
                }

                return View(group);
            }
            return RedirectToAction("Login", "Login");

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id,Group cv)
        {
            if (id != cv.GroupId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _groupStoredRepo.Update(cv.GroupId, cv.GroupCode,cv.Description,cv.Status,cv.FromDate.Value,cv.ToDate.Value);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
            //if (id != null)
            //{
            //    _groupStoredRepo.Update(cv);
            //    return RedirectToAction("Index", "GroupStored");
            //}

            //return RedirectToAction("Create", "GroupStored");
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
                _groupStoredRepo.Delete(id);
                return RedirectToAction("Index", "Group");
            }
            catch
            {
                return RedirectToAction("Index", "Group");
            }
        }
    }
}
