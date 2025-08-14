using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Validations;

namespace Equinox.Models.DomainModels
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, ErrorMessage = "Name must be 50 characters or less.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d ]+$", ErrorMessage = "Name must be alphanumeric.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone must be in 123-456-7890 format.")]
        [UniquePhoneNumber(ErrorMessage = "Phone number already exists.")]
        [Remote(action: "CheckPhone", controller: "Validation", areaName: "Admin", ErrorMessage = "Phone number already exists.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a date of birth.")]
        [MinimumAge(8, ErrorMessage = "Age must be between 8 and 80.", MaxAge = 80)]
        [Remote(action: "ValidateDOB", controller: "User", areaName: "Admin", ErrorMessage = "Date of Birth must be in the past and age must be valid.")]
        public DateTime DOB { get; set; }

        public bool IsCoach { get; set; } = false;
    }
}
