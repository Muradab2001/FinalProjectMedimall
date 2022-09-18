using FinalProjectMedimall.Models.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMedimall.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public List<Medicine> Medicines { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
