namespace PharmaReachMVC.ViewModels
{
    public class MedicineSearchViewModel
    {
        public string PageTitle { get; set; }
        public string SearchQuery { get; set; }
        public string ActionUrl { get; set; }
        // Any other properties like filters you may want to pass, e.g., Country, City
    }
}
