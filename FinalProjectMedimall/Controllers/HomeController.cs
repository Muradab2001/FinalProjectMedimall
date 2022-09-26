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
                Medicines = _context.Medicines.ToList(),
                Discount=  _context.Discounts.FirstOrDefault(d=>d.Id==2)
            };
            return View(homeVM);
        }
    }
}
