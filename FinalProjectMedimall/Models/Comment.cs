using FinalProjectMedimall.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System;

namespace FinalProjectMedimall.Models
{
    public class Comment:BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsAccess { get; set; }
        public int? BookId { get; set; }
        public Medicine Medicine { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
