using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinalProjectMedimall.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(Contact message)
        {
            if (!ModelState.IsValid) return View();
            Contact contact = new Contact
            {
                Id = message.Id,
                Subject = message.Subject,
                Email = message.Email,
                Name = message.Name,
                Date = DateTime.Now,
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
