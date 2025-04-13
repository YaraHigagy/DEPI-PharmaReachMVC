using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public OrdersController(PharmaReachDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var pharmaReachDbContext = _context.Orders.Include(o => o.Customer).Include(o => o.Pharmacy);
            return View(await pharmaReachDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Pharmacy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email");
            return View();
        }
        
        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,CustomerId,PharmacyId,CreatedAt,UpdatedAt,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", order.CustomerId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", order.PharmacyId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", order.CustomerId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", order.PharmacyId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,CustomerId,PharmacyId,CreatedAt,UpdatedAt,TotalPrice")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", order.CustomerId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", order.PharmacyId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Pharmacy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        //public async Task<IActionResult> AddToCart(int medicineId)
        //{
        //    // Get the current logged-in customer
        //    var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(customerId))
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    // Check if the customer already has an active order (in progress)
        //    var existingOrder = await _context.Orders
        //        .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.Status == OrderStatus.InProgress);

        //    if (existingOrder == null)
        //    {
        //        // If no active order, create a new order
        //        var newOrder = new Order
        //        {
        //            CustomerId = customerId,
        //            Status = OrderStatus.InProgress, // Use the enum value here
        //            CreatedAt = DateTime.Now,
        //            TotalPrice = 0,
        //            // Assuming the pharmacy ID is part of the logged-in customer or elsewhere; you can adjust this as needed
        //            PharmacyId = 1, // This should come from a valid source, such as the customer's default pharmacy
        //            Name = "New Order", // This could be dynamically set or passed as part of the user request
        //        };

        //        _context.Orders.Add(newOrder);
        //        await _context.SaveChangesAsync();

        //        existingOrder = newOrder;
        //    }

        //    // Get the medicine details
        //    var medicine = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == medicineId);
        //    if (medicine == null)
        //    {
        //        return NotFound();
        //    }

        //    // Add the medicine to the order details
        //    var orderDetail = new OrderDetail
        //    {
        //        OrderId = existingOrder.Id,
        //        MedicineId = medicine.Id,
        //        Quantity = 1,
        //        PricePerItem = medicine.Price,
        //        TotalPricePerItem = medicine.Price
        //    };

        //    _context.OrderDetails.Add(orderDetail);
        //    existingOrder.TotalPrice += medicine?.Price ?? 0;

        //    // Update the order with the new total price and date
        //    existingOrder.UpdatedAt = DateTime.Now;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Details", "Orders", new { id = existingOrder.Id });
        //}

    }
}
