using PharmaReachMVC.Models;

namespace PharmaReachMVC.ViewModels
{
    public class PharmacyProfileViewModel
    {
        public Pharmacy Pharmacy { get; set; }
        public List<Order> Orders { get; set; }
        public string PharmacyInitials { get; set; }
    }

}
