using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents medicines in a pharmacy that can be offered for free under certain conditions.
    /// </summary>
    public class MedicinePharmacyCanBeFree
    {
        [Required]
        public int PharmacyId { get; set; }

        [ForeignKey(nameof(PharmacyId))]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [ForeignKey(nameof(MedicineId))]
        public Medicine Medicine { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available quantity must be greater than or equal to 0.")]
        public int AvailableQuantity { get; set; }
    }
}
