using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class OrderItem:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? MedicineId { get; set; }
        public string AppUserId { get; set; }
        public int OrderId { get; set; }
        public Medicine Medicine { get; set; }
        public AppUser AppUser { get; set; }
        public Order Order { get; set; }
    }
}
