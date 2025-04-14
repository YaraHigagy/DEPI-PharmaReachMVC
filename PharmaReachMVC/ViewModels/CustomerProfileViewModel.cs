using PharmaReachMVC.Models;

namespace PharmaReachMVC.ViewModels
{
    public class CustomerProfileViewModel
    {
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public string CustomerInitials { get; set; }
    }

}
