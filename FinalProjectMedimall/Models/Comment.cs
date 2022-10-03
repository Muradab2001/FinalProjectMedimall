using FinalProjectMedimall.Models.Base;
using System;

namespace FinalProjectMedimall.Models
{
    public class Comment:BaseEntity
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public Medicine Medicine { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int MedicineId { get; set; }
    }
}
