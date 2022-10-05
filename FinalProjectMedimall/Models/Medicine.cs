using FinalProjectMedimall.Models.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMedimall.Models
{
    public class Medicine:BaseEntity
    {
        public string Image { get; set; }
        [Required, StringLength(maximumLength: 30)]
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
  
        public string Article { get; set; }
        public string Desc { get; set; }

        public int RateAvg { get; set; }
        public int Sellcount { get; set; }
        public bool Trend { get; set; } = false;
        public bool Stock { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Rate> Rates { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<MedicineImage> MedicineImages{ get; set; }


        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
        [NotMapped]
        public List<int> ImagesId { get; set; }

    }
}
