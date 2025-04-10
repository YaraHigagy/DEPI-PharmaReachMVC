using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents the relationship between a pharmacy and a medicine that is available for free in that pharmacy.
    /// Tracks the available quantity of the medicine in the pharmacy.
    /// </summary>
    public class MedicinePharmacyIsFree
    {
        [Required]
        public int PharmacyId { get; set; }

        [ForeignKey(nameof(PharmacyId))]
        public Pharmacy Pharmacy { get; set; }

        [ForeignKey(nameof(MedicineId))]
        [Required]
        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available quantity must be greater than or equal to 0.")]
        public int AvailableQuantity { get; set; }
    }
}
