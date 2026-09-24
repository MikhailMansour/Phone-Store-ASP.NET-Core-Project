using System.ComponentModel.DataAnnotations;

namespace Products.ViewModels.Authantication
{
    public class RegisterViewModel
    {
        //[Required]
        //public string SSN { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Please select a role")]
        public string Role { get; set; }
    }
}
