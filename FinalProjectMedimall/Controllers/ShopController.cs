using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
          
            HomeVM homeVM = new HomeVM
            {
                Medicines=await _context.Medicines.Include(m=>m.Category).ToListAsync(),
                Categories=await _context.Categories.ToListAsync()
            };

            return View(homeVM);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return View();
            Order order=_context.Orders.FirstOrDefault(o => o.Id == id);
            return View(order);
        }
    }
}
