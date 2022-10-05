using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class TrendController : Controller
    {
      
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TrendController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Medicines.Count() / 5);
            ViewBag.CurrentPage = page;
            List<Medicine> model = await _context.Medicines.Include(c => c.Category).Include(c => c.MedicineImages).Skip((page - 1) * 5).Take(5).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Trend(int id)
        {
            Medicine medicine = _context.Medicines.FirstOrDefault(m => m.Id == id);
            if (medicine.Trend==false)
            {
                medicine.Trend = true;
            }
            else if (medicine.Trend==true)
            {
                medicine.Trend = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
