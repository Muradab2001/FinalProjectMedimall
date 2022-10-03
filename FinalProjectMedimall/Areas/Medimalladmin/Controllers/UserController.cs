using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<AppUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Users.Count() / 5);
            ViewBag.CurrentPage = page;
            List<AppUser> user = _userManager.Users.Skip((page - 1) * 5).Take(5).ToList();

            return View(user);
        }
        public async Task<IActionResult> UserStatusChange(string id)
        {
          
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user.Admin == null)
            {
                user.Admin = false;


                await _userManager.RemoveFromRoleAsync(user, "Member");
                await _userManager.AddToRoleAsync(user, "Moderator");

            }
            else if (user.Admin == false)
            {
                user.Admin = true;

                await _userManager.RemoveFromRoleAsync(user, "Moderator");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else if (user.Admin == true)
            {
                user.Admin = null;

                await _userManager.RemoveFromRoleAsync(user, "Admin");

                await _userManager.AddToRoleAsync(user, "Member");
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
