using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserType { get; set; }

    }
}
