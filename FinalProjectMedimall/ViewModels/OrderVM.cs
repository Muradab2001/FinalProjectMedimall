using FinalProjectMedimall.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMedimall.ViewModels
{
    public class OrderVM
    {
  
        public string FristName { get; set; }

        public string LastName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
