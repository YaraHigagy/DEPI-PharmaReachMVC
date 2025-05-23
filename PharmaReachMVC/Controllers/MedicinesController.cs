﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC;
using PharmaReachMVC.Helpers;
using PharmaReachMVC.Models;
using PharmaReachMVC.Utilities;
using PharmaReachMVC.ViewModels;

namespace PharmaReachMVC.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly PharmaReachDbContext _context;

        public MedicinesController(PharmaReachDbContext context)
        {
            _context = context;
        }

        #region GET: Medicines
        //// GET: Medicines
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Medicines.ToListAsync());
        //}

        //// GET: Medicines
        //public async Task<IActionResult> Index(string searchQuery, bool? isFreeFilter = null, bool? canBeFreeFilter = null, int page = 1)
        //{
        //    int pageSize = 8;
        //    var query = _context.Medicines.AsQueryable();

        //    // Apply search query if provided
        //    if (!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        query = query.Where(m => m.Name.Contains(searchQuery) || m.Description.Contains(searchQuery));
        //    }

        //    // Apply active tab filters:
        //    if (canBeFreeFilter.HasValue)
        //    {
        //        query = query.Where(m => _context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id) == canBeFreeFilter.Value);
        //    }
        //    else if (isFreeFilter.HasValue)
        //    {
        //        query = query.Where(m => _context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id) == isFreeFilter.Value);
        //    }

        //    // Pagination logic
        //    var totalItems = await query.CountAsync();
        //    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        //    var medicines = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        //    var medicineViewModels = medicines.Select(m => new MedicineViewModel
        //    {
        //        Medicine = m,
        //        IsFree = _context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id),
        //        CanBeFree = _context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id)
        //    }).ToList();

        //    ViewData["CurrentPage"] = page;
        //    ViewData["TotalPages"] = totalPages;
        //    ViewData["SearchQuery"] = searchQuery;
        //    ViewData["IsFreeFilter"] = isFreeFilter;
        //    ViewData["CanBeFreeFilter"] = canBeFreeFilter;

        //    return View(medicineViewModels);

        public async Task<IActionResult> Index(string searchQuery, bool? isFreeFilter = null, bool? canBeFreeFilter = null, int page = 1)
        {
            int pageSize = 8;
            var query = _context.Medicines.AsQueryable();

            // Use the helper method
            query = MedicineQueryHelper.ApplySearchAndFilters(_context, query, searchQuery, isFreeFilter, canBeFreeFilter);

            // Pagination
            var paginatedMedicines = await PaginatedList<Medicine>.CreateAsync(query, page, pageSize);

            var medicineViewModels = paginatedMedicines.Select(m => new MedicineViewModel
            {
                Medicine = m,
                IsFree = _context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id),
                CanBeFree = _context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id)
            }).ToList();

            ViewData["CurrentPage"] = paginatedMedicines.CurrentPage;
            ViewData["TotalPages"] = paginatedMedicines.TotalPages;
            ViewData["SearchQuery"] = searchQuery;
            ViewData["IsFreeFilter"] = isFreeFilter;
            ViewData["CanBeFreeFilter"] = canBeFreeFilter;

            var searchViewModel = new MedicineSearchViewModel
            {
                PageTitle = "Medicines",
                SearchQuery = searchQuery,
                ActionUrl = Url.Action("Index", "Medicines")
            };

            return View("Index", new Tuple<List<MedicineViewModel>, MedicineSearchViewModel>(medicineViewModels, searchViewModel));
        }

        #endregion

        #region GET: Medicines/PrepaidMedicines
        // GET: Medicines/PrepaidMedicines
        //public async Task<IActionResult> PrepaidMedicines()
        //{
        //    var medicineIds = await _context.MedicinePharmacyIsFrees
        //        .Select(f => f.MedicineId)
        //        .Distinct()
        //        .ToListAsync();

        //    var medicines = await _context.Medicines
        //        .Where(m => medicineIds.Contains(m.Id))
        //        .ToListAsync();

        //    var medicineViewModels = medicines.Select(m => new MedicineViewModel
        //    {
        //        Medicine = m,
        //        IsFree = true,
        //        CanBeFree = _context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id)
        //    }).ToList();

        //    return View(medicineViewModels);
        //}

        public async Task<IActionResult> PrepaidMedicines(string searchQuery, bool? canBeFreeFilter, bool? isFreeFilter, int page = 1)
        {
            int pageSize = 8;

            var query = _context.Medicines.AsQueryable();

            // Only include prepaid medicines
            query = query.Where(m => _context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id));

            // Apply search and filters
            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(m => m.Name.Contains(searchQuery));

            var paginatedMedicines = await PaginatedList<Medicine>.CreateAsync(query, page, pageSize);

            var medicineViewModels = paginatedMedicines
                .Select(m => MedicineViewModelMapper.Map(_context, m))
                .ToList();

            // Pass pagination info to View
            ViewData["CurrentPage"] = paginatedMedicines.CurrentPage;
            ViewData["TotalPages"] = paginatedMedicines.TotalPages;
            ViewData["SearchQuery"] = searchQuery;
            ViewData["CanBeFreeFilter"] = canBeFreeFilter;
            ViewData["IsFreeFilter"] = isFreeFilter;

            var searchViewModel = new MedicineSearchViewModel
            {
                PageTitle = "Prepaid Medicines",
                ActionUrl = Url.Action("PrepaidMedicines", "Medicines"),
                SearchQuery = searchQuery,
            };

            return View("PrepaidMedicines", new Tuple<List<MedicineViewModel>, MedicineSearchViewModel>(medicineViewModels, searchViewModel));
        }

        #endregion

        #region // GET: Medicines/CharitableMedicines
        // GET: Medicines/CharitableMedicines
        //public async Task<IActionResult> CharitableMedicines()
        //{
        //    var medicineIds = await _context.MedicinePharmacyCanBeFrees
        //        .Select(f => f.MedicineId)
        //        .Distinct()
        //        .ToListAsync();

        //    var medicines = await _context.Medicines
        //        .Where(m => medicineIds.Contains(m.Id))
        //        .ToListAsync();

        //    var medicineViewModels = medicines.Select(m => new MedicineViewModel
        //    {
        //        Medicine = m,
        //        IsFree = _context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id),
        //        CanBeFree = true
        //    }).ToList();

        //    return View(medicineViewModels);
        //}

        public async Task<IActionResult> CharitableMedicines(string searchQuery, bool? canBeFreeFilter, bool? isFreeFilter, int page = 1)
        {
            int pageSize = 8;

            var query = _context.Medicines.AsQueryable();

            // Apply search and filters
            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(m => m.Name.Contains(searchQuery));

            // Only include charitable medicines
            query = query.Where(m => _context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id));

            // Apply pagination (assuming you have a helper or extension for PaginatedList)
            var paginatedMedicines = await PaginatedList<Medicine>.CreateAsync(query, page, pageSize);

            var medicineViewModels = paginatedMedicines
                .Select(m => MedicineViewModelMapper.Map(_context, m))
                .ToList();

            // Set ViewData for pagination links
            ViewData["CurrentPage"] = paginatedMedicines.CurrentPage;
            ViewData["TotalPages"] = paginatedMedicines.TotalPages;
            ViewData["SearchQuery"] = searchQuery;
            ViewData["CanBeFreeFilter"] = canBeFreeFilter;
            ViewData["IsFreeFilter"] = isFreeFilter;

            // 👇👇 This is the part you're asking about
            var searchViewModel = new MedicineSearchViewModel
            {
                PageTitle = "Charitable Medicines",
                ActionUrl = Url.Action("CharitableMedicines", "Medicines"),
                SearchQuery = searchQuery,
            };

            return View(new Tuple<List<MedicineViewModel>, MedicineSearchViewModel>(medicineViewModels, searchViewModel));
        }


        #endregion

        // GET: Medicines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Medicines/DetailsPartial/5
        public async Task<IActionResult> DetailsPartial(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicine == null)
            {
                return NotFound();
            }

            return PartialView("_MedicineDetailsPartial", medicine);
        }


        // GET: Medicines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageUrl,CreatedAt,UpdatedAt")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageUrl,CreatedAt,UpdatedAt")] Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.Id))
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
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}
