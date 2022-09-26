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
    public class DiscountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Medicines = _context.Medicines.Include(m => m.Discount).ToList();
            List<Discount> discounts = _context.Discounts.ToList();
            return View(discounts);
        }
        public IActionResult GetMedicine(int? id)
        {
            ViewBag.Medicines = _context.Medicines.Include(m => m.Discount).ToList();
            Discount discounts = _context.Discounts.FirstOrDefault(d=>d.Id==id);
            return View(discounts);
        }
        public IActionResult edit(int? id)
        {
            if (id is null) return NotFound();
            Discount discounts = _context.Discounts.FirstOrDefault(d => d.Id == id);
            return View(discounts);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async  Task<IActionResult> edit(int? id,Discount discount)
        {
            if (id is null) return NotFound();
            Discount exsited = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            if (exsited == null) return NotFound();
            _context.Entry(exsited).CurrentValues.SetValues(discount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
