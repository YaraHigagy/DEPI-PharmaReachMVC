using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaReachMVC.Models
{
    /// <summary>
    /// Represents the details of an order, including the medicine, quantity, and price information.
    /// </summary>
    public class OrderDetail
    {
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [ForeignKey(nameof(MedicineId))]
        public Medicine Medicine { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 999999999999999999.99, ErrorMessage = "Price per item must be greater than 0.")]
        public decimal PricePerItem { get; set; }

        [Required]
        [Range(0.01, 999999999999999999.99, ErrorMessage = "Total price per item must be greater than 0.")]
        public decimal TotalPricePerItem { get; set; }
    }
}
