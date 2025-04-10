using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents a medicine product available for purchase and pharmacy stock.
    /// </summary>
    public class Medicine
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 999999999999999999.99)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } // Not required, nullable

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<PharmacyMedicine> PharmacyMedicines { get; set; }

        // Navigation property for MedicinePharmacyCanBeFree
        public ICollection<MedicinePharmacyCanBeFree> MedicinePharmacyCanBeFrees { get; set; }

        // Navigation property for MedicinePharmacyIsFree
        public ICollection<MedicinePharmacyIsFree> MedicinePharmacyIsFrees { get; set; }
    }
}
