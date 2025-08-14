using System.ComponentModel.DataAnnotations;

namespace Equinox.Models.DomainModels
{
    public class ClassCategory
    {
        public int ClassCategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, ErrorMessage = "Name must be 50 characters or less.")]
        [RegularExpression(@"^[A-Za-z\d ]+$", ErrorMessage = "Name must be alphanumeric.")]
        public string Name { get; set; } = string.Empty;

        public string? Image { get; set; }
    }
}
