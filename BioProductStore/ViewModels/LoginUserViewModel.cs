using System.ComponentModel.DataAnnotations;

namespace BioProductStore.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}