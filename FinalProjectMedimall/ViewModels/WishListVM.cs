using FinalProjectMedimall.Models;
using System.Collections.Generic;

namespace FinalProjectMedimall.ViewModels
{
    public class WishListVM
    {
        public List<WishListItemVM> WishListItemVMs { get; set; }
        public int Count { get; set; }
    }
}
