using FinalProjectMedimall.Models;
using System.Collections.Generic;

namespace FinalProjectMedimall.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }

        public List<Medicine> Medicines { get; set; }
        public List<Order> Orders { get; set; }
        public Discount Discount { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
