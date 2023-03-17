using B3_C1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Formats.Asn1.AsnWriter;
using System;
using DAL_B3.DomainClass;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace B3_C1.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Index(string keyword, string filter)
        {
            var data = _groupService.GetAll();

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

        public IActionResult Filter(string filter)
        {
            var url = $"/Group/Index?filter={filter}";

            if (filter == null)
            {
                url = $"/Group/Index";
            }
            else
            {
                url = $"/Group/Index?filter={filter}";
            }

            return Json(new { status = "success", redirectUrl = url });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group cv)
        {
            if (ModelState.IsValid)
            {
                cv.Status = true;
                var userCreate = HttpContext.Session.GetString("UserName");
                cv.CreatedPerson = userCreate;
                _groupService.Create(cv);
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Create", "Group");
        }

        [HttpGet]
        public IActionResult Update(Guid Id)
        {
            return View(_groupService.GetById(Id));
        }

        [HttpPost]
        public IActionResult Update(Guid id, Group cv)
        {
            if (cv != null)
            {
                _groupService.Update(id, cv);
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Create", "Group");
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
                _groupService.Delete(id);
                return RedirectToAction("Index", "Group");
            }
            catch
            {
                return RedirectToAction("Index", "Group");
            }
        }

    }
}
