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
    public class PharmacyMedicinesController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public PharmacyMedicinesController(PharmaReachDbContext context)
        {
            _context = context;
        }

        // GET: PharmacyMedicines
        public async Task<IActionResult> Index()
        {
            var pharmaReachDbContext = _context.PharmacyMedicines.Include(p => p.Medicine).Include(p => p.Pharmacy);
            return View(await pharmaReachDbContext.ToListAsync());
        }

        // GET: PharmacyMedicines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyMedicine = await _context.PharmacyMedicines
                .Include(p => p.Medicine)
                .Include(p => p.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (pharmacyMedicine == null)
            {
                return NotFound();
            }

            return View(pharmacyMedicine);
        }

        // GET: PharmacyMedicines/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email");
            return View();
        }

        // POST: PharmacyMedicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PharmacyId,MedicineId,QuantityAvailable,PriceOverride")] PharmacyMedicine pharmacyMedicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacyMedicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", pharmacyMedicine.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", pharmacyMedicine.PharmacyId);
            return View(pharmacyMedicine);
        }

        // GET: PharmacyMedicines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyMedicine = await _context.PharmacyMedicines.FindAsync(id);
            if (pharmacyMedicine == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", pharmacyMedicine.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", pharmacyMedicine.PharmacyId);
            return View(pharmacyMedicine);
        }

        // POST: PharmacyMedicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PharmacyId,MedicineId,QuantityAvailable,PriceOverride")] PharmacyMedicine pharmacyMedicine)
        {
            if (id != pharmacyMedicine.PharmacyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacyMedicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyMedicineExists(pharmacyMedicine.PharmacyId))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", pharmacyMedicine.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", pharmacyMedicine.PharmacyId);
            return View(pharmacyMedicine);
        }

        // GET: PharmacyMedicines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyMedicine = await _context.PharmacyMedicines
                .Include(p => p.Medicine)
                .Include(p => p.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (pharmacyMedicine == null)
            {
                return NotFound();
            }

            return View(pharmacyMedicine);
        }

        // POST: PharmacyMedicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacyMedicine = await _context.PharmacyMedicines.FindAsync(id);
            if (pharmacyMedicine != null)
            {
                _context.PharmacyMedicines.Remove(pharmacyMedicine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyMedicineExists(int id)
        {
            return _context.PharmacyMedicines.Any(e => e.PharmacyId == id);
        }
    }
}
