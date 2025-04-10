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
    public class MedicinePharmacyIsFreesController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public MedicinePharmacyIsFreesController(PharmaReachDbContext context)
        {
            _context = context;
        }

        // GET: MedicinePharmacyIsFrees
        public async Task<IActionResult> Index()
        {
            var pharmaReachDbContext = _context.MedicinePharmacyIsFrees.Include(m => m.Medicine).Include(m => m.Pharmacy);
            return View(await pharmaReachDbContext.ToListAsync());
        }

        // GET: MedicinePharmacyIsFrees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyIsFree = await _context.MedicinePharmacyIsFrees
                .Include(m => m.Medicine)
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (medicinePharmacyIsFree == null)
            {
                return NotFound();
            }

            return View(medicinePharmacyIsFree);
        }

        // GET: MedicinePharmacyIsFrees/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email");
            return View();
        }

        // POST: MedicinePharmacyIsFrees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PharmacyId,MedicineId,AvailableQuantity")] MedicinePharmacyIsFree medicinePharmacyIsFree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinePharmacyIsFree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyIsFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyIsFree.PharmacyId);
            return View(medicinePharmacyIsFree);
        }

        // GET: MedicinePharmacyIsFrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyIsFree = await _context.MedicinePharmacyIsFrees.FindAsync(id);
            if (medicinePharmacyIsFree == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyIsFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyIsFree.PharmacyId);
            return View(medicinePharmacyIsFree);
        }

        // POST: MedicinePharmacyIsFrees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PharmacyId,MedicineId,AvailableQuantity")] MedicinePharmacyIsFree medicinePharmacyIsFree)
        {
            if (id != medicinePharmacyIsFree.PharmacyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinePharmacyIsFree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinePharmacyIsFreeExists(medicinePharmacyIsFree.PharmacyId))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyIsFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyIsFree.PharmacyId);
            return View(medicinePharmacyIsFree);
        }

        // GET: MedicinePharmacyIsFrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyIsFree = await _context.MedicinePharmacyIsFrees
                .Include(m => m.Medicine)
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (medicinePharmacyIsFree == null)
            {
                return NotFound();
            }

            return View(medicinePharmacyIsFree);
        }

        // POST: MedicinePharmacyIsFrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicinePharmacyIsFree = await _context.MedicinePharmacyIsFrees.FindAsync(id);
            if (medicinePharmacyIsFree != null)
            {
                _context.MedicinePharmacyIsFrees.Remove(medicinePharmacyIsFree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinePharmacyIsFreeExists(int id)
        {
            return _context.MedicinePharmacyIsFrees.Any(e => e.PharmacyId == id);
        }
    }
}
