using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
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


        public async Task<IActionResult> Index(int? id, int key = 0, int page = 1, int max = 0, int min = 0, int point = 0)
        {
            ViewBag.CurrentKey = key;
            ViewBag.CurrentPage = page;
            ViewBag.Max = max;
            ViewBag.Min = min;
            ViewBag.Point=point;
            ViewBag.CurrentCategory = id;
            HomeVM home = new HomeVM
            {
                Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                Discount = _context.Discounts.FirstOrDefault(d => d.Id == 2),

            };
            if (id == null)
            {
                home.Medicines = _context.Medicines.Include(m => m.Rates).ToList();
            }
            else
            {
                home.Medicines = _context.Medicines.Include(m => m.Rates).Where(m => m.CategoryId == id).ToList();
            }
            ViewBag.TotalPage = Math.Ceiling((decimal)home.Medicines.Count() / 4);

            if (point != 0)
                {
                List<Medicine> existed = home.Medicines.Where(m => m.RateAvg == point).ToList();
                home.Medicines = existed;
                    ViewBag.TotalPage = Math.Ceiling((decimal)home.Medicines.Count() / 4);

            }
            if (max != 0 || min != 0)
            {
                home.Medicines = home.Medicines.Where(m => m.Price > min && m.Price < max).ToList();
                ViewBag.TotalPage = Math.Ceiling((decimal)home.Medicines.Count() / 4);

            }

            switch (key)
                    {
                    case 0:

                        home.Medicines = home.Medicines.Skip((page - 1) * 4).Take(4).ToList();
                        break;
                    case 2:
                   
                           
                    home.Medicines = home.Medicines.OrderBy(Medicines => Medicines.Name).Skip((page - 1) * 4).Take(4).ToList();
                    break;
                        case 3:
                    home.Medicines = home.Medicines.OrderByDescending(Medicines => Medicines.Price).Skip((page - 1) * 4).Take(4).ToList();

                    break;
                        case 4:
                            home.Medicines = home.Medicines.OrderBy(Medicines => Medicines.Price).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                        default:
                            home.Medicines = home.Medicines.OrderBy(Medicines => Medicines.Id).Skip((page - 1) * 4).Take(4).ToList();

                            break;

                    }

          return View(home);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return View();
            Order order= await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return View(order);
        }


    }
}
