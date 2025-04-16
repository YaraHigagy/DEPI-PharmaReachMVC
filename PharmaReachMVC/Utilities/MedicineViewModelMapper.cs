using PharmaReachMVC.Models;
using PharmaReachMVC.ViewModels;

namespace PharmaReachMVC.Helpers
{
    public static class MedicineViewModelMapper
    {
        public static MedicineViewModel Map(PharmaReachDbContext context, Medicine medicine)
        {
            return new MedicineViewModel
            {
                Medicine = medicine,
                IsFree = context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == medicine.Id),
                CanBeFree = context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == medicine.Id)
            };
        }
    }
}
