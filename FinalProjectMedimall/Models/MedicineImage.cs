using FinalProjectMedimall.Models.Base;

namespace FinalProjectMedimall.Models
{
    public class MedicineImage:BaseEntity
    {
        public string Name { get; set; }
        public string Alternative { get; set; }
        public bool IsMain { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
