using System;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Models.Validations
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _min;
        public int MaxAge { get; set; } = int.MaxValue;

        public MinimumAgeAttribute(int min)
        {
            _min = min;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dob)
                return new ValidationResult("Date of birth is required.");

            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;

            return age >= _min && age <= MaxAge
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? $"Age must be between {_min} and {MaxAge}.");
        }
    }
}
