using System.ComponentModel.DataAnnotations;

namespace FinalProjectMedimall.ViewModels
{
    public class LoginVM
    {
        [Required, StringLength(25)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
