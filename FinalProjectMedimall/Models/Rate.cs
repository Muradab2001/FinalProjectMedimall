using FinalProjectMedimall.Models.Base;
using System;

namespace FinalProjectMedimall.Models
{
    public class Rate:BaseEntity
    {
        public DateTime Date { get; set; }
        public int Point { get; set; }
        public Medicine Medicine { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int MedicineId { get; set; }
    }
}
