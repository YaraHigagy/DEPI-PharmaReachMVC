using PharmaReachMVC.Models;

namespace PharmaReachMVC.Utilities
{
    public class MedicineQueryHelper
    {
        public static IQueryable<Medicine> ApplySearchAndFilters(
            PharmaReachDbContext context,
            IQueryable<Medicine> query,
            string searchQuery,
            bool? isFreeFilter,
            bool? canBeFreeFilter)
        {
            // Apply search query if provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(m => m.Name.Contains(searchQuery) || m.Description.Contains(searchQuery));
            }

            // Apply active tab filters
            if (canBeFreeFilter.HasValue)
            {
                query = query.Where(m =>
                    context.MedicinePharmacyCanBeFrees.Any(f => f.MedicineId == m.Id) == canBeFreeFilter.Value);
            }
            else if (isFreeFilter.HasValue)
            {
                query = query.Where(m =>
                    context.MedicinePharmacyIsFrees.Any(f => f.MedicineId == m.Id) == isFreeFilter.Value);
            }

            return query;
        }
    }
}
