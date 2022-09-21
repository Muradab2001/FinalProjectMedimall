using FinalProjectMedimall.Models;
using System.Collections.Generic;

namespace FinalProjectMedimall.ViewModels
{
    public class LayoutBasketVM
    {
        public List<BasketCookieItemVM> BasketCookieItemVMs { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketItemVM> BasketItemVMs { get; set; }
    }
}
