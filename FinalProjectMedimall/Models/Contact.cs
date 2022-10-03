using FinalProjectMedimall.Models.Base;
using System;

namespace FinalProjectMedimall.Models
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public bool Look { get; set; }
        public DateTime Date { get; set; }
    }
}
