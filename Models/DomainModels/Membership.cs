using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Equinox.Models
{
    public class Membership
    {
        public int MembershipID { get; set; }

        [Required(ErrorMessage = "Please enter a membership name.")]
        [StringLength(40)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100000, ErrorMessage = "Please enter a valid price.")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
    }
}
