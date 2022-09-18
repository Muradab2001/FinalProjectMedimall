using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class Rate:BaseEntity
    {
        public byte Point { get; set; }
        public Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
    }
}
