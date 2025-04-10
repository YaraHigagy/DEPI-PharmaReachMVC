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
    public class MedicinePharmacyCanBeFreesController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public MedicinePharmacyCanBeFreesController(PharmaReachDbContext context)
        {
            _context = context;
        }

        // GET: MedicinePharmacyCanBeFrees
        public async Task<IActionResult> Index()
        {
            var pharmaReachDbContext = _context.MedicinePharmacyCanBeFrees.Include(m => m.Medicine).Include(m => m.Pharmacy);
            return View(await pharmaReachDbContext.ToListAsync());
        }

        // GET: MedicinePharmacyCanBeFrees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyCanBeFree = await _context.MedicinePharmacyCanBeFrees
                .Include(m => m.Medicine)
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (medicinePharmacyCanBeFree == null)
            {
                return NotFound();
            }

            return View(medicinePharmacyCanBeFree);
        }

        // GET: MedicinePharmacyCanBeFrees/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name");
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email");
            return View();
        }

        // POST: MedicinePharmacyCanBeFrees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PharmacyId,MedicineId,AvailableQuantity")] MedicinePharmacyCanBeFree medicinePharmacyCanBeFree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinePharmacyCanBeFree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyCanBeFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyCanBeFree.PharmacyId);
            return View(medicinePharmacyCanBeFree);
        }

        // GET: MedicinePharmacyCanBeFrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyCanBeFree = await _context.MedicinePharmacyCanBeFrees.FindAsync(id);
            if (medicinePharmacyCanBeFree == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyCanBeFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyCanBeFree.PharmacyId);
            return View(medicinePharmacyCanBeFree);
        }

        // POST: MedicinePharmacyCanBeFrees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PharmacyId,MedicineId,AvailableQuantity")] MedicinePharmacyCanBeFree medicinePharmacyCanBeFree)
        {
            if (id != medicinePharmacyCanBeFree.PharmacyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinePharmacyCanBeFree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinePharmacyCanBeFreeExists(medicinePharmacyCanBeFree.PharmacyId))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "Id", "Name", medicinePharmacyCanBeFree.MedicineId);
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies, "Id", "Email", medicinePharmacyCanBeFree.PharmacyId);
            return View(medicinePharmacyCanBeFree);
        }

        // GET: MedicinePharmacyCanBeFrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinePharmacyCanBeFree = await _context.MedicinePharmacyCanBeFrees
                .Include(m => m.Medicine)
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.PharmacyId == id);
            if (medicinePharmacyCanBeFree == null)
            {
                return NotFound();
            }

            return View(medicinePharmacyCanBeFree);
        }

        // POST: MedicinePharmacyCanBeFrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicinePharmacyCanBeFree = await _context.MedicinePharmacyCanBeFrees.FindAsync(id);
            if (medicinePharmacyCanBeFree != null)
            {
                _context.MedicinePharmacyCanBeFrees.Remove(medicinePharmacyCanBeFree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinePharmacyCanBeFreeExists(int id)
        {
            return _context.MedicinePharmacyCanBeFrees.Any(e => e.PharmacyId == id);
        }
    }
}
