using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MedicineController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Detail(int? id)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.discount = _context.Discounts.FirstOrDefault(d => d.Id == 2);
            Medicine medicine = _context.Medicines.Include(m => m.Category).Include(m => m.MedicineImages).Include(m=>m.Comments).ThenInclude(x => x.AppUser).Include(x=>x.Rates).ThenInclude(x=>x.AppUser).FirstOrDefault(m => m.Id == id);
            return View(medicine);
        }
        public async Task<IActionResult> DeleteBasketitem(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<BasketItem> basketItems = _context.BasketItems.Where(m => m.MedicineId == id && m.AppUserId == user.Id).ToList();
                foreach (var item in basketItems)
                {
                    _context.BasketItems.Remove(item);
                }
            }
            else
            {
                string basket = HttpContext.Request.Cookies["Basket"];

                List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                BasketCookieItemVM cookieItem = basketCookieItems.FirstOrDefault(c => c.Id == id);


                basketCookieItems.Remove(cookieItem);

                string basketStr = JsonConvert.SerializeObject(basketCookieItems);

                HttpContext.Response.Cookies.Append("Basket", basketStr);

            }
            _context.SaveChanges();
            return PartialView("_basketPartial");
        }
        public async Task<IActionResult> removeCartItem(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                List<BasketItem> basketItems = _context.BasketItems.Where(m => m.MedicineId == Id && m.AppUserId == user.Id).ToList();
                if (basketItems == null) return Json(new { status = 404 });
                foreach (var item in basketItems)
                {

                    _context.BasketItems.Remove(item);
                }
            }

            _context.SaveChanges();


            return Json(new { status = 200 });
        }
        public async Task<IActionResult> decrease(int Id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            BasketItem basket = _context.BasketItems.Include(m=> m.Medicine).ThenInclude(m => m.Discount).FirstOrDefault(m => m.MedicineId == Id && m.AppUserId == user.Id);
            int quantity = 0;
           
            if (basket.Quantity == 1)
            {
                basket.Quantity = 1;
            }
            else
            {
                basket.Quantity--;
            }
            _context.SaveChanges();
            decimal TotalPrice = 0;
            decimal Price = basket.Quantity * basket.Medicine.DiscountId == 2 ? (basket.Price * basket.Medicine.Discount.Percentage) / 100 : basket.Price;
            List<BasketItem> basketItems = _context.BasketItems.Include(m => m.AppUser).Include(m=> m.Medicine).Where(b => b.AppUserId == user.Id).ToList();
            quantity = basket.Quantity;
            foreach (BasketItem item in basketItems)
            {
                Medicine medicine = _context.Medicines.Include(m => m.Discount).FirstOrDefault(m => m.Id == item.MedicineId);

                BasketItemVM basketItemVM = new BasketItemVM
                {
                    Medicine = medicine,
                    Quantity = item.Quantity
                };
                basketItemVM.Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price;
                TotalPrice += basketItemVM.Price * basketItemVM.Quantity;

            }

            return Json(new { totalPrice = TotalPrice, Price, quantity });
        }
        public IActionResult GetPartial()
        {
            return PartialView("_basketPartial");
        }
        [HttpPost]
        public async Task<IActionResult> increase(int Id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            BasketItem basket = _context.BasketItems.Include(m=> m.Medicine).ThenInclude(m=>m.Discount).FirstOrDefault(m => m.MedicineId == Id && m.AppUserId == user.Id);
            int quantity = 0;
            basket.Quantity++;
     
            decimal TotalPrice = 0;
            decimal Price = basket.Quantity * basket.Medicine.DiscountId == 2 ? (basket.Price * basket.Medicine.Discount.Percentage) / 100 : basket.Price;
            List<BasketItem> basketItems = _context.BasketItems.Include(m => m.AppUser).Include(m => m.Medicine).ThenInclude(m=>m.Discount).Where(m => m.AppUserId == user.Id).ToList();
            quantity = basket.Quantity;
            _context.SaveChanges();
            foreach (BasketItem item in basketItems)
            {
                Medicine medicine = _context.Medicines.Include(m => m.Category).Include(m=>m.Discount).FirstOrDefault(m=> m.Id == item.MedicineId);

                BasketItemVM basketItemVM = new BasketItemVM
                {
                    Medicine = medicine,
                    Quantity = item.Quantity,
                   
                 };
                basketItemVM.Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price;
                TotalPrice += basketItemVM.Price * basketItemVM.Quantity;

            }

            return Json(new { totalPrice = TotalPrice, Price, quantity });
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            Medicine medicine = _context.Medicines.Include(m => m.MedicineImages).Include(m=>m.Discount).Include(m => m.Category).FirstOrDefault(m => m.Id == id);
            if(medicine == null)return View("Error");

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                BasketItem basketItem = _context.BasketItems.FirstOrDefault(m => m.MedicineId == medicine.Id && m.AppUserId == user.Id);
                if (basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = user.Id,
                        Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price,
                        MedicineId = medicine.Id,
                        Quantity = 1
                    };
                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    basketItem.Quantity++;
                }
                _context.SaveChanges();

                return PartialView("_basketPartial");
            }
            else
            {
                string basket = HttpContext.Request.Cookies["Basket"];

                if (basket == null)
                {
                    List<BasketCookieItemVM> basketCookieItems = new List<BasketCookieItemVM>();

                    basketCookieItems.Add(new BasketCookieItemVM
                    {
                        Id = medicine.Id,
                        Quantity = 1
                    });

                    string basketStr = JsonConvert.SerializeObject(basketCookieItems);


                    HttpContext.Response.Cookies.Append("Basket", basketStr);
                    return PartialView("_basketPartial");

                }
                else
                {
                    List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                    BasketCookieItemVM cookieItem = basketCookieItems.FirstOrDefault(c => c.Id == medicine.Id);

                    if (cookieItem == null)
                    {
                        cookieItem = new BasketCookieItemVM
                        {
                            Id = medicine.Id,
                            Quantity = 1
                        };
                        basketCookieItems.Add(cookieItem);
                    }
                    else
                    {
                        cookieItem.Quantity++;
                    }



                    string basketStr = JsonConvert.SerializeObject(basketCookieItems);

                    HttpContext.Response.Cookies.Append("Basket", basketStr);
                    return PartialView("_basketPartial");

                }
            }
        }
        public async Task<IActionResult> AddWishlist(int id)
        {
            Medicine medicine = _context.Medicines.Include(m=>m.Category).Include(m=>m.MedicineImages).FirstOrDefault(m => m.Id == id);

            if (User.Identity.IsAuthenticated && User.IsInRole("Member"))
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                WishListItem wishlistItem = _context.WishListItems.FirstOrDefault(m=> m.MedicineId == medicine.Id && m.AppUserId == user.Id);
                if (wishlistItem == null)
                {
                    wishlistItem = new WishListItem
                    {
                        AppUserId = user.Id,
                        MedicineId = medicine.Id,
                        Count = 1,
                    };
                    _context.WishListItems.Add(wishlistItem);
                }
                else
                {
                    wishlistItem.Count = 1;
                }
                _context.SaveChanges();
                return PartialView("_WishlistPartialView");
            }
            else
            {
                string wishlist = HttpContext.Request.Cookies["Wishlist"];

                if (wishlist == null)
                {
                    List<WishListCookieItemVM> wishlistCookieItems = new List<WishListCookieItemVM>();

                    wishlistCookieItems.Add(new WishListCookieItemVM
                    {
                        Id = medicine.Id,
                        Count = 1
                    });

                    string wishlistStr = JsonConvert.SerializeObject(wishlistCookieItems);

                    HttpContext.Response.Cookies.Append("Wishlist", wishlistStr);
                    return PartialView("_WishlistPartialView");

                }
                else
                {
                    List<WishListCookieItemVM> wishlistCookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishlist);

                    WishListCookieItemVM cookieItem = wishlistCookieItems.FirstOrDefault(b => b.Id == medicine.Id);

                    if (cookieItem == null)
                    {
                        cookieItem = new WishListCookieItemVM
                        {
                            Id = medicine.Id,
                            Count = 1
                        };
                        wishlistCookieItems.Add(cookieItem);
                    }
                    else
                    {
                        cookieItem.Count = 1;
                    }

                    string wishlistStr = JsonConvert.SerializeObject(wishlistCookieItems);

                    HttpContext.Response.Cookies.Append("Wishlist", wishlistStr);

                    return PartialView("_WishlistPartialView");
                }
            }
        }

        public IActionResult GetWishlistPartial()
        {
            return PartialView("_WishlistPartialView");
        }

        public async Task<IActionResult> DeleteWishListItem(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<WishListItem> wishlistItems = _context.WishListItems.Where(m => m.MedicineId == id && m.AppUserId == user.Id).ToList();
                foreach (var item in wishlistItems)
                {
                    _context.WishListItems.Remove(item);
                }
            }
            else
            {
                string basket = HttpContext.Request.Cookies["Wishlist"];

                List<WishListCookieItemVM> wishlistCookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(basket);

                WishListCookieItemVM cookieItem = wishlistCookieItems.FirstOrDefault(c => c.Id == id);


                wishlistCookieItems.Remove(cookieItem);

                string wishlistStr = JsonConvert.SerializeObject(wishlistCookieItems);

                HttpContext.Response.Cookies.Append("Wishlist", wishlistStr);

            }
            _context.SaveChanges();
            return PartialView("_WishlistPartialView");
        }

        [HttpGet]
        public async Task<IActionResult> AddRate(int id,int point)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);       
            if (user == null) return RedirectToAction("Login", "Contact");
            Rate Rate = new Rate
            {
                Date= DateTime.Now,
              AppUser =user,
              MedicineId=id,
              Point=point
            };
            _context.Rates.Add(Rate);
            _context.SaveChanges();

            Medicine medicine = _context.Medicines.FirstOrDefault(c => c.Id == id);
            List<Rate> rates = _context.Rates.Include(r => r.Medicine).Where(r => r.MedicineId == id).ToList();
            int pointrate = 0;
            foreach (var item in rates)
            {
                pointrate += item.Point;
            }
            if (rates.Count > 0)
            {
                medicine.RateAvg = pointrate / rates.Count;
                _context.SaveChanges();
            }


            return Json("ok");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Contact");
            if (comment.Message is null) return RedirectToAction("Detail", "Medicine", new { id = comment.MedicineId });
            if (!ModelState.IsValid) return RedirectToAction("Detail", "Medicine", new { id = comment.MedicineId });
            if (!_context.Medicines.Any(f => f.Id == comment.MedicineId)) return NotFound();
            Comment cmnt = new Comment
            {
                Message = comment.Message,
                MedicineId = comment.MedicineId,
                Date = DateTime.Now,
                AppUserId = user.Id,
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Medicine", new { id = comment.MedicineId });
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Detail", "medicine");
            if (User.IsInRole("Admin"))
            {
                Comment commentadmin = _context.Comments.FirstOrDefault(c => c.Id == id);
                _context.Comments.Remove(commentadmin);
                _context.SaveChanges();
                return RedirectToAction("Detail", "Medicine", new { id = commentadmin.MedicineId });
            }
            if (!_context.Comments.Any(c => c.Id == id && c.AppUserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Medicine", new { id = comment.MedicineId });
        }
        public async Task<IActionResult> DeleteRate(int id, int medid)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Detail", "Medicine");
            Medicine medicine = _context.Medicines.FirstOrDefault(c => c.Id == medid);
            if (User.IsInRole("Admin"))
            {
                Rate rateadmin = _context.Rates.FirstOrDefault(c => c.Id == id);
                _context.Rates.Remove(rateadmin);
                _context.SaveChanges();
                List<Rate> ratesadmin = _context.Rates.Include(r => r.Medicine).Where(r => r.MedicineId == medid).ToList();
                int pointrateadmin = 0;
                if (ratesadmin.Count == 0)
                {
                    medicine.RateAvg = 0;
                }
                else
                {
                    foreach (var item in ratesadmin)
                    {
                        pointrateadmin += item.Point;
                    }
                    medicine.RateAvg = pointrateadmin / ratesadmin.Count;
                }
                _context.SaveChanges();
                return RedirectToAction("Detail", "Medicine", new { id = medicine.Id });
            }
            else if (!_context.Rates.Any(c => c.Id == id && c.AppUserId == user.Id)) return NotFound();
            Rate rate = _context.Rates.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Rates.Remove(rate);
            _context.SaveChanges();
            List<Rate> rates = _context.Rates.Include(r => r.Medicine).Where(r => r.MedicineId == medid).ToList();

            int pointrate = 0;
            if (rates.Count==0)
            {
                medicine.RateAvg = 0;
            }
            else
            {
                foreach (var item in rates)
                {
                    pointrate += item.Point;
                }
                medicine.RateAvg = pointrate / rates.Count;
            }
            _context.SaveChanges();
            return RedirectToAction("Detail", "Medicine", new { id = medicine.Id });
        }

    }
}
