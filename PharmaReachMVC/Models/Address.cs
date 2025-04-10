using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents a physical address used by customers and pharmacies for deliveries.
    /// </summary>
    public class Address
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [Required]
        [StringLength(255)]
        public string Street { get; set; }

        [Required]
        [StringLength(255)]
        public string Building { get; set; }

        [StringLength(255)]
        public string Apartment { get; set; }

        public string Notes { get; set; }

        public string DeliveryInstructions { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Pharmacy> Pharmacies { get; set; }
    }
}
