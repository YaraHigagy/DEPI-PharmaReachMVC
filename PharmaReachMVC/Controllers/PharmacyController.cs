using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;
using PharmaReachMVC.Utilities;
using PharmaReachMVC.ViewModels;

namespace PharmaReachMVC.Controllers
{
    // Authorize the user and ensure they have the "Pharmacy" role
    [Authorize(Roles = "Pharmacy")]
    public class PharmacyController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public PharmacyController(PharmaReachDbContext context)
        {
            _context = context;
        }

        // GET: Pharmacy/Profile
        public IActionResult Profile(string id)  // Using string to allow for GUID if necessary
        {
            var viewModel = GetPharmacyProfileViewModel(id);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        // POST: Pharmacy/Profile (AJAX request for updating order status)
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

        // Helper function to get the pharmacy profile view model based on the pharmacy ID
        private PharmacyProfileViewModel GetPharmacyProfileViewModel(string pharmacyId)
        {
            var pharmacy = _context.Pharmacies
                .Include(p => p.Address)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.Customer)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Medicine)
                .FirstOrDefault(p => p.Id.ToString() == pharmacyId);  // Adjust for string ID

            if (pharmacy == null) return null;

            var viewModel = new PharmacyProfileViewModel
            {
                Pharmacy = pharmacy,
                Orders = pharmacy.Orders.ToList(),
                PharmacyInitials = StringUtilities.GetInitialsFromFullName(pharmacy.Name)
            };

            return viewModel;
        }

        // POST method to update order status from the Pharmacy's profile page
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

        // Helper class to map the posted JSON (for AJAX requests)
        public class ProfilePostData
        {
            public bool ajax { get; set; }
            public int orderId { get; set; }
            public int newStatus { get; set; }
            public string id { get; set; }  // Pharmacy id (string type for GUID support)
        }
    }
}
