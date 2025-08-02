using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Models
{
    public class Club
    {
        public int ClubId { get; set; }

        [Required(ErrorMessage = "Please enter a club name.")]
        [StringLength(50, ErrorMessage = "Name must be 50 characters or less.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d ]+$", ErrorMessage = "Name must be alphanumeric.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in 123-456-7890 format.")]
       // [Remote(action: "CheckPhone_Club", controller: "Validation", areaName: "Admin", ErrorMessage = "Phone number already exists.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
