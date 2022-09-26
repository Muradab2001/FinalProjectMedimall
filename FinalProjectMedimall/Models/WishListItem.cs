using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class WishListItem:BaseEntity
    {
        public int MedicineId { get; set; }
        public string AppUserId { get; set; }
        public int Count { get; set; }
        public Medicine Medicine { get; set; }
        public AppUser AppUser { get; set; }
    }
}
