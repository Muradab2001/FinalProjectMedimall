using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace FinalProjectMedimall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
    
        }
        public IActionResult Index(string str)
        {
           
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                Medicines = _context.Medicines.Include(m=>m.Rates).ToList(),
                Rates = _context.Rates.ToList(),
                Discount=  _context.Discounts.FirstOrDefault(d=>d.Id==2)
            };
            if (!string.IsNullOrEmpty(str))
            {
                homeVM.Medicines.Where(m => m.Name.Trim().ToLower().Contains(str)).ToList();
            }
            return View(homeVM);
        }
        public IActionResult Error()
        {
          
            return View();
        }
    }
}
