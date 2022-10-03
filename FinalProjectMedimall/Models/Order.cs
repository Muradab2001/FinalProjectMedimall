using System.Collections.Generic;
using System;
using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class Order:BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public bool Archive { get; set; } = false;
        public string City { get; set; }
        public bool? Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
