using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents the relationship between a pharmacy and the medicines it has, 
    /// along with their available quantity and optional price override.
    /// </summary>
    public class PharmacyMedicine
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
        [Range(0, int.MaxValue, ErrorMessage = "QuantityAvailable must be a positive number.")]
        public int QuantityAvailable { get; set; }

        public decimal? PriceOverride { get; set; }
    }
}
