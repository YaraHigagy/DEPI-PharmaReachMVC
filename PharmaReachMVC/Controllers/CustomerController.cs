using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;
using PharmaReachMVC.Utilities;
using PharmaReachMVC.ViewModels;

namespace PharmaReachMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public CustomerController(PharmaReachDbContext context)
        {
            _context = context;
        }

        public IActionResult Profile(int id = 4)
        {
            var customer = _context.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Pharmacy)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Medicine)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerProfileViewModel
            {
                Customer = customer,
                Orders = customer.Orders.ToList(),
                CustomerInitials = StringUtilities.GetInitialsFromFullName(customer.Name)
            };

            return View(viewModel);
        }
    }
}
