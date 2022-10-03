using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.basketitem = await _context.OrderItems.Include(o=>o.Medicine).ToListAsync();
            if(id == null)return NotFound();
            Order order =_context.Orders.FirstOrDefault(o => o.Id == id);
            if (order is null) return View();
            return View(order);
        }
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Orders.Count() / 10);
            ViewBag.CurrentPage = page;
    
            List<Order> order = await  _context.Orders.Include(o=>o.OrderItems).Include(o=>o.AppUser).Skip((page - 1) * 10).Take(10).ToListAsync();
            foreach (Order item in order)
            {
                if (item.Date.AddDays(30)<DateTime.Now)
                {
                    if (item.Status==true)
                    {
                        item.Archive = true;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return View(order);
        }
        public async Task<IActionResult> Success(int? id)
        {
            if (id == null) return View();
            Order existed = _context.Orders.Include(o=>o.OrderItems).Include(o=>o.AppUser).FirstOrDefault(o=>o.Id == id);

            existed.Status = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
             Order order =_context.Orders.FirstOrDefault(o=>o.Id==id);
            if (order == null) return View();
             _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> archive(int? id)
        {
            if (id==null)
            {
                List<Order> orderindex = await _context.Orders.Include(o => o.OrderItems).Include(o => o.AppUser).ToListAsync();
                if (orderindex is null) return View();
                return View(orderindex);
            }
            Order order = _context.Orders.FirstOrDefault(O => O.Id == id);
            if(order is null) return View();
            order.Archive = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
