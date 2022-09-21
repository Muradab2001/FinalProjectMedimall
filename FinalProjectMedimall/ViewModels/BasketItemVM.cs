using FinalProjectMedimall.Models;
using System.Collections.Generic;

namespace FinalProjectMedimall.ViewModels
{
    public class BasketItemVM
    {
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
