using Microsoft.AspNetCore.Identity;

namespace PharmaReachMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Role: Can be "Pharmacy" or "Customer"
        public string UserType { get; set; }

        public Pharmacy Pharmacy { get; set; }
        public Customer Customer { get; set; }

    }
}
