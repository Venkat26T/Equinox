using Equinox.Models.DomainModels;

namespace Equinox.Models.ViewModels
{
    public class FilterViewModel
    {
        public List<EquinoxClass> AvailableClasses { get; set; } = new List<EquinoxClass>();

        public List<Club> AllClubs { get; set; } = new List<Club>();

        public List<ClassCategory> AllCategories { get; set; } = new List<ClassCategory>();

        public string SelectedClubName { get; set; } = "All";

        public string SelectedCategoryName { get; set; } = "All";
    }
}
