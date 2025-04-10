using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents a country that contains multiple cities and addresses.
    /// </summary>
    public class Country
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
