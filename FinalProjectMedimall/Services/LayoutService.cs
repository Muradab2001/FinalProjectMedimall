using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using FinalProjectMedimall.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Services
{
    public class LayoutService
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContext = httpContextAccessor;
            _userManager = userManager;
        }
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = _context.Contacts.ToList();
            return contacts;
        }
        public List<Category> GetCategories()
        {
           List<Category> categories = _context.Categories.ToList();
            return categories;
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = _context.Orders.ToList();
            return orders;
        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = _context.Settings.ToList();
            return settings;
        }
        public async Task<BasketVM> ShowBasket()
        {
            string basket = _httpContext.HttpContext.Request.Cookies["Basket"];

            BasketVM basketData = new BasketVM
            {
                TotalPrice = 0,
                BasketItems = new List<BasketItemVM>(),
                Count = 0
            };
            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);
                List<BasketItem> basketItems = _context.BasketItems.Include(b => b.AppUser).Where(b => b.AppUserId == user.Id).ToList();
                foreach (BasketItem item in basketItems)
                {
                    Medicine medicine= _context.Medicines.Include(m => m.MedicineImages).Include(m=>m.Category).Include(m=>m.Discount).FirstOrDefault(m => m.Id == item.MedicineId);
                    if (medicine != null)
                    {

                        BasketItemVM basketItemVM = new BasketItemVM
                        {
                            Medicine = medicine,
                            Quantity = item.Quantity,
                        };
                        basketItemVM.Price = medicine.DiscountId==2? (medicine.Price * medicine.Discount.Percentage) / 100 :medicine.Price;
                        basketData.BasketItems.Add(basketItemVM);
                        basketData.Count++;
                        basketData.TotalPrice += basketItemVM.Price * basketItemVM.Quantity;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(basket))
                {
                    List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                    foreach (BasketCookieItemVM item in basketCookieItems)
                    {
                        Medicine medicine = _context.Medicines.FirstOrDefault(f => f.Id == item.Id);
                        if (medicine != null)
                        {
                            BasketItemVM basketItem = new BasketItemVM
                            {
                                Medicine = _context.Medicines.Include(m => m.MedicineImages).Include(m => m.Category).Include(m => m.Discount).FirstOrDefault(m => m.Id == item.Id),
                                Quantity = item.Quantity

                            };
                            basketItem.Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price;
                            basketData.BasketItems.Add(basketItem);
                            basketData.Count++;
                            basketData.TotalPrice += basketItem.Price * basketItem.Quantity;
                        }
                    }

                }
            }

            return basketData;

        }
        public async Task<WishListVM> GetWishlist()
        {
            string wishlist = _httpContext.HttpContext.Request.Cookies["Wishlist"];

            WishListVM wishlistData = new WishListVM
            {
                WishListItemVMs = new List<WishListItemVM>(),
                Count = 0
            };
            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);
                List<WishListItem> wishlistItems = _context.WishListItems.Include(b => b.AppUser).Where(b => b.AppUserId == user.Id).ToList();
                foreach (WishListItem item in wishlistItems)
                {
                    Medicine medicine = _context.Medicines.Include(m => m.Category).Include(m => m.MedicineImages).Include(m=>m.Discount).FirstOrDefault(m => m.Id == item.MedicineId);
                    if (medicine != null)
                    {
                        WishListItemVM wishlistItemVM = new WishListItemVM
                        {
                            Medicine = medicine,
                            Count = item.Count
                        };
                        wishlistItemVM.Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price;
                        wishlistData.WishListItemVMs.Add(wishlistItemVM);
                        wishlistData.Count++;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(wishlist))
                {
                    List<WishListCookieItemVM> wishlistCookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishlist);

                    foreach (WishListCookieItemVM item in wishlistCookieItems)
                    {
                        Medicine medicine = _context.Medicines.Include(m => m.Category).Include(m=>m.Discount).Include(m => m.MedicineImages).FirstOrDefault(m => m.Id == item.Id);
                        if (medicine != null)
                        {
                            WishListItemVM wishlistItem = new WishListItemVM
                            {
                             Medicine = _context.Medicines.Include(m => m.Category).Include(m => m.MedicineImages).FirstOrDefault(m => m.Id == item.Id),
                            Count = item.Count
                            };

                            wishlistItem.Price = medicine.DiscountId == 2 ? (medicine.Price * medicine.Discount.Percentage) / 100 : medicine.Price;
                            wishlistData.WishListItemVMs.Add(wishlistItem);
                            wishlistItem.Count++;
                        }
                    }

                }
            }
            return wishlistData;
        }
    }
}
