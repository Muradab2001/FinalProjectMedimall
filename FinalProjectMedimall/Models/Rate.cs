using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class Rate:BaseEntity
    {
        public byte Point { get; set; }
        public Medicine Medicine { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int MedicineId { get; set; }
    }
}
