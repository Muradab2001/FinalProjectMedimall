using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Contact> contact = await _context.Contacts.ToListAsync();
            foreach (var item in contact)
            {
                item.Look = true;
            }
            _context.SaveChanges();
            return View(contact);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return View("Error");
            Contact contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null) return View("Error");
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
