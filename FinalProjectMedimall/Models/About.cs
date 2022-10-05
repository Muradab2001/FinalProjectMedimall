using FinalProjectMedimall.Models.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMedimall.Models
{
    public class About : BaseEntity
    {
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public byte Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
