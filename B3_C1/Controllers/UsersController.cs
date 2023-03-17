using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL_B3.Context;
using DAL_B3.DomainClass;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace B3_C1.Controllers
{
    public class UsersController : Controller
    {
        private readonly BaiTap2_DbContext _context;

        public UsersController(BaiTap2_DbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string filter)
        {
            var data = await _context.Users.Include(u => u.RoleId_Navigation).ToListAsync();

            if (filter == "2")
            {
                data = await _context.Users.Where(c => c.Status == true).ToListAsync();
            }
            else if (filter == "1")
            {
                data = await _context.Users.Where(c => c.Status == false).Include(u => u.RoleId_Navigation).ToListAsync();
            }
            else
            {
                data = await _context.Users.ToListAsync();
            }
            List<SelectListItem> isStatus = new List<SelectListItem>();

            isStatus.Add(new SelectListItem() { Text = "Tất cả", Value = "0" });
            isStatus.Add(new SelectListItem() { Text = "Đã xóa", Value = "1" });
            isStatus.Add(new SelectListItem() { Text = "Chưa xóa", Value = "2" });

            ViewData["isStatus"] = isStatus;

            return View(data);
        }


        // GET: Users/Create
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

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,RoleId,Status,Description,CreatedPerson,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                
                user.Status = true;
                var userCreate = HttpContext.Session.GetString("UserName");
                user.CreatedPerson = userCreate;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleCode", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleCode", user.RoleId);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("UserId,UserName,Password,RoleId,Status,Description,CreatedPerson,Phone")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleCode", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var userCreate = HttpContext.Session.GetString("UserName");
            if (userCreate != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.Users
                    .Include(u => u.RoleId_Navigation)
                    .FirstOrDefaultAsync(m => m.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            return RedirectToAction("Login", "Login");
            
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var user = await _context.Users.FindAsync(id);
            user.Status = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid? id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
