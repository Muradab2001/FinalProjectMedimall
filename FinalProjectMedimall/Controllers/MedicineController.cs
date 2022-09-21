using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProjectMedimall.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicineController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            ViewBag.Category = _context.Categories.ToList();
            Medicine medicine=_context.Medicines.Include(m=>m.Category).Include(m=>m.MedicineImages).SingleOrDefault(m=>m.Id==id);
            return View(medicine);
        }
    }
}
