using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.Models
{
    public enum OrderStatus
    {
        Pending = 0,  // Order has been placed but not yet processed.
        Processing = 1,  // Order is being prepared or packed for shipment.
        Shipped = 2,  // Order has been shipped and is on its way to the customer.
        Delivered = 3,  // Order has been delivered to the customer.
        Cancelled = 4,  // Order has been cancelled by the customer or the store.
        Returned = 5  // Order has been returned by the customer after delivery.
    }

    /// <summary>
    /// Represents an order placed by a customer at a pharmacy.
    /// </summary>
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public OrderStatus Status { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [Required]
        public int PharmacyId { get; set; }

        [ForeignKey(nameof(PharmacyId))]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } // Not required, nullable

        [Required]
        [Range(0.01, 999999999999999999.99, ErrorMessage = "Total price must be greater than 0.")]
        public decimal TotalPrice { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
