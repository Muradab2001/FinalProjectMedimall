using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IActionResult> Index(int? id, string sortingOrder, int page = 1)
        {
            int max = 2;
            if (id != 0 || id != null)
            {
                List<Medicine> furnitures = new List<Medicine>();
                //furnitures = GetDatas(sorting);
                Category category = await _context.Categories
                .Include(c => c.Medicines).Skip((page - 1) * 4).Take(4)
                .FirstOrDefaultAsync(x => x.Id == id);
                double pageCount = Math.Ceiling((double)((decimal)_context.Medicines.Count() / Convert.ToDecimal(max)));
                ViewBag.CurentPage = page;
                ViewBag.TotalPage = pageCount;
                if (category != null)
                {
                    if (category.Medicines.Count() != 0)
                    {
                        HomeVM home = new HomeVM
                        {
                            Categories = _context.Categories.ToList(),
                            Medicines = category.Medicines
                        };
                        return View(home);
                    }
                    else
                    {
                        ViewBag.Message = "category";
                        return View();
                    }
                }
            }

            HomeVM homeVM = new HomeVM
            {
                Categories = _context.Categories.ToList(),
                Medicines = _context.Medicines.Include(f => f.Category).Skip((page - 1) * max).Take(max).ToList()
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
