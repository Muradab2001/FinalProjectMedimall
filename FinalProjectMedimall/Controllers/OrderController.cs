using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Controllers
{
    //[Authorize(Roles = "Member")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewCart()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Contact");

            OrderVM model = new OrderVM
            {

                FristName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(m => m.Medicine).Where(m => m.AppUserId == user.Id).ToList(),

            };
           
            return View(model);

        }
        public async Task<IActionResult> Checkout()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            OrderVM model = new OrderVM
            {
                FristName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(m => m.Medicine).Where(m => m.AppUserId == user.Id).ToList()

            };
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM orderVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            OrderVM model = new OrderVM
            {
                FristName = user.FirstName,
                LastName =user.LastName,
                Username = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(m => m.Medicine).ThenInclude(m=>m.Discount).Where(m => m.AppUserId == user.Id).ToList()
            };
            if (!ModelState.IsValid) return View(model);

            TempData["Succeeded"] = false;

            if (model.BasketItems.Count == 0) return RedirectToAction("index", "home");
            Order order = new Order
            {
                Country = orderVM.Country,
                Address = orderVM.Address,
                Status=false,
                Archive=false,
                City = orderVM.City,
                Phone =orderVM.Phone,
                TotalPrice = 0,
                Date = DateTime.Now,
                AppUserId = user.Id
            };

            foreach (BasketItem item in model.BasketItems)
            {
                order.TotalPrice += item.Price * item.Quantity;
                OrderItem orderItem = new OrderItem
                {
                    Name = item.Medicine.Name,
                    Price = item.Price,
                    Quantity=item.Quantity,
                    AppUserId = user.Id,
                    MedicineId = item.Medicine.Id,
                    Order = order
                };
                List<Medicine> medicines = _context.Medicines.ToList();
                foreach (var medicine in medicines)
                {
                    if (item.MedicineId == medicine.Id)
                    {
                        medicine.Sellcount += item.Quantity;
                    }
                }
                _context.OrderItems.Add(orderItem);
            }
            _context.BasketItems.RemoveRange(model.BasketItems);
            _context.Orders.Add(order);
            _context.SaveChanges();
            TempData["Succeeded"] = true;

            return RedirectToAction("index", "home");
        }
    }
}
