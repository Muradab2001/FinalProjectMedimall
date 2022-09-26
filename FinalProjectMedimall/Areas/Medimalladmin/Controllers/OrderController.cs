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
            if(id == null)return NotFound();
            Order order =_context.Orders.FirstOrDefault(o => o.Id == id);
            if (order is null) return View();
            return View(order);
        }
        public async Task<IActionResult> Index()
        {
            List<Order> order = await  _context.Orders.Include(o=>o.OrderItems).Include(o=>o.AppUser).ToListAsync();
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
        public async Task<IActionResult> archive()
        {
            List<Order> order = await _context.Orders.Include(o => o.OrderItems).Include(o => o.AppUser).ToListAsync();
            return View(order);
        }
    }
}
