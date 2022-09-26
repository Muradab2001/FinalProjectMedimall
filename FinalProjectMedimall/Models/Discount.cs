using FinalProjectMedimall.Models.Base;
using System;
using System.Collections.Generic;

namespace FinalProjectMedimall.Models
{
    public class Discount:BaseEntity
    {
        public decimal Percentage { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
