using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents a pharmacy entity with information about the pharmacy and its address.
    /// </summary>
    public class Pharmacy
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } // Not required, nullable

        [Required]
        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        // Navigation property for the ApplicationUser
        public string ApplicationUserId { get; set; }  // Link to the ApplicationUser
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<PharmacyMedicine> PharmacyMedicines { get; set; }

        // Navigation property for MedicinePharmacyCanBeFree
        public ICollection<MedicinePharmacyCanBeFree> MedicinePharmacyCanBeFrees { get; set; }

        // Navigation property for MedicinePharmacyIsFree
        public ICollection<MedicinePharmacyIsFree> MedicinePharmacyIsFrees { get; set; }
    }
}
