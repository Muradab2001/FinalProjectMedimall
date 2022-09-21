using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalProjectMedimall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                Medicines = _context.Medicines.ToList()
            };
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Test(int id)
        {
            Medicine medicine = _context.Medicines.Include(m=>m.MedicineImages).Include(m=>m.Category).FirstOrDefault(m=>m.Id==id);
            return Json(medicine);
        }
    }
}
