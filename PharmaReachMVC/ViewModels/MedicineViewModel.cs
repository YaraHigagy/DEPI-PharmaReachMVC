using PharmaReachMVC.Models;

namespace PharmaReachMVC.ViewModels
{
    public class MedicineViewModel
    {
        public Medicine Medicine { get; set; }
        public bool IsFree { get; set; }
        public bool CanBeFree { get; set; }
    }
}
