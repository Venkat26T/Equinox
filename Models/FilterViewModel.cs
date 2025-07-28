using System.Collections.Generic;

namespace Equinox.Models.ViewModels
{
    public class FilterViewModel
    {
        public List<EquinoxClass> AvailableClasses { get; set; } = new();

        public List<Club> AllClubs { get; set; } = new();

        public List<ClassCategory> AllCategories { get; set; } = new();

        public string SelectedClubName { get; set; } = "All";

        public string SelectedCategoryName { get; set; } = "All";
    }
}
