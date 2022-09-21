using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class BasketItem: BaseEntity
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
