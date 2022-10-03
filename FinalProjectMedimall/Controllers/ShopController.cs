using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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

        public async Task<IActionResult> Index(int? id, string sortingOrder, int page = 1, int max = 0, int min = 0, int point = 0)
        {


                     HomeVM home = new HomeVM
                     {
                         Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                         Medicines = _context.Medicines.Include(m => m.Rates).Include(f => f.Category).Skip((page - 1) * 4).Take(4).ToList()
                     };
                  if (id == null)
                   {

                
                        ViewBag.TotalPage = Math.Ceiling((decimal)_context.Medicines.Where(m=>m.DiscountId==3).Count() / 4);
                        ViewBag.CurrentPage = page;
                          if (point!=0)
                          {
                           List<Rate> rates=_context.Rates.Include(r=>r.Medicine).Where(r=>r.Point==point).ToList();
                    List<Medicine> medicines = _context.Medicines.Include(m => m.Rates).ToList();
                    List<Medicine> existed = new List<Medicine>();
                    foreach (var medicine in medicines)
                    {
                        foreach (var rate in rates)
                        {
                            if (medicine.Id==rate.MedicineId)
                            {
                                existed.Add(medicine);
                            }
                        }
                    }
                    home.Medicines = existed;
                    return View(home);
                            }
                          if (sortingOrder!=null)
                          {
                          HomeVM sort = new HomeVM
                          {
                        Medicines = _context.Medicines.Include(f => f.Category).Include(m=>m.Rates).ThenInclude(f=>f.Point).ToList(),
                        Categories = _context.Categories.Include(c => c.Medicines).ToList(),

                          };
                    switch (sortingOrder)
                    {
                        case "name_desc":
                            sort.Medicines = sort.Medicines.OrderByDescending(Medicines => Medicines.Name).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                        case "Date":
                            sort.Medicines = sort.Medicines.OrderBy(Medicines => Medicines.Name).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                        case "date_desc":
                            sort.Medicines = sort.Medicines.OrderBy(Medicines => Medicines.Price).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                        case "Price by descending":
                            sort.Medicines = sort.Medicines.OrderByDescending(Medicines => Medicines.Price).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                        default:
                            sort.Medicines = sort.Medicines.OrderBy(Medicines => Medicines.Id).Skip((page - 1) * 4).Take(4).ToList();

                            break;
                    }
                    return View(sort);
                    }

                       if (max!=0||min !=0)
                       {
                        ViewBag.TotalPage = Math.Ceiling((decimal)_context.Medicines.Where(m => m.Price > min && m.Price < max&&m.DiscountId==3).Count() / 4);
                       ViewBag.CurrentPage = page;
                        HomeVM filter = new HomeVM
                        {
                        Medicines = _context.Medicines.Include(m=>m.Rates).Include(f => f.Category).Where(m => m.Price > min && m.Price < max).Skip((page - 1) * 4).Take(4).ToList(),
                        Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                        };
                        return View(filter);
                }
                HomeVM homeVM = new HomeVM
                {
                    Categories = _context.Categories.Include(c=>c.Medicines).ToList(),
                    Medicines = _context.Medicines.Include(m => m.Rates).Include(f => f.Category).Skip((page - 1) * 4).Take(4).ToList()
                };
                home = homeVM;
                       }
            else
            {
                List<Medicine> furnitures = new List<Medicine>();
                Category category = await _context.Categories.Include(c => c.Medicines).ThenInclude(m=>m.Rates).FirstOrDefaultAsync(x => x.Id == id);
               
                ViewBag.TotalPage = Math.Ceiling((decimal)category.Medicines.Where(m=>m.DiscountId==3).Count() / 4);
                ViewBag.CurrentPage = page;
                if (point != 0)
                {
                    List<Rate> rates = _context.Rates.Include(r => r.Medicine).Where(r => r.Point == point).ToList();
                    List<Medicine> medicines = _context.Medicines.Include(m => m.Rates).ToList();
                    List<Medicine> existed = new List<Medicine>();
                    foreach (var medicine in medicines)
                    {
                        foreach (var rate in rates)
                        {
                            if (medicine.Id == rate.MedicineId)
                            {
                                existed.Add(medicine);
                            }
                        }
                    }
                    home.Medicines = existed;
                    return View(home);
                }
                if (category != null)
                {
                    if (category.Medicines.Count() != 0)
                    {
                        HomeVM homeVM = new HomeVM
                        {
                            Categories = _context.Categories.Include(c => c.Medicines).ToList(),
                            Medicines = category.Medicines.Skip((page - 1) * 4).Take(4).ToList(),
                        };
                        return View(homeVM);
                        
                    }
          

                }
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
