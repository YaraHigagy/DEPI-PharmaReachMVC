using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
