using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;
using PharmaReachMVC.Utilities;
using PharmaReachMVC.ViewModels;

namespace PharmaReachMVC.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public PharmacyController(PharmaReachDbContext context)
        {
            _context = context;
        }

        public IActionResult Profile(int id = 4)
        {
            var viewModel = GetPharmacyProfileViewModel(id);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile([FromBody] ProfilePostData data)
        {
            if (data.ajax)
            {
                var order = _context.Orders.Find(data.orderId);
                if (order == null)
                    return NotFound(new { message = "Order not found." });

                order.Status = (OrderStatus)data.newStatus;
                order.UpdatedAt = DateTime.Now;
                _context.SaveChanges();

                return Ok(new { message = "Order status updated successfully." });
            }

            // If NOT an AJAX request (should not happen) - just reload profile
            var viewModel = GetPharmacyProfileViewModel(data.id);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        private PharmacyProfileViewModel GetPharmacyProfileViewModel(int pharmacyId)
        {
            var pharmacy = _context.Pharmacies
                .Include(p => p.Address)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.Customer)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Medicine)
                .FirstOrDefault(p => p.Id == pharmacyId);

            if (pharmacy == null) return null;

            var viewModel = new PharmacyProfileViewModel
            {
                Pharmacy = pharmacy,
                Orders = pharmacy.Orders.ToList(),
                PharmacyInitials = StringUtilities.GetInitialsFromFullName(pharmacy.Name)
            };

            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return NotFound();

            order.Status = newStatus;
            order.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            // Respond with a success message
            return Ok(new { message = "Status updated successfully." });
        }

        // Helper class to map the posted JSON
        public class ProfilePostData
        {
            public bool ajax { get; set; }
            public int orderId { get; set; }
            public int newStatus { get; set; }
            public int id { get; set; }  // Pharmacy id (optional, if needed)
        }
    }
}
