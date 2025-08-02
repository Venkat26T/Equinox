using System;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Models
{
   /* public class MinimumAgeAttribute : ValidationAttribute
    {
        public int MinimumAge { get; }
        public int MaxAge { get; set; } = int.MaxValue;

        public MinimumAgeAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {


            if (value == null)
                return new ValidationResult("Date of birth is required.");

            if (value is DateTime dob)
            {
                var today = DateTime.Today;
                int age = today.Year - dob.Year;

                if (dob > today.AddYears(-age))
                {
                    age--; // adjust for birth date not yet reached this year
                }


                Console.WriteLine($"DOB received: {dob.ToShortDateString()} | Age calculated: {age}");
                if (age < MinimumAge || age > MaxAge)
                {
                    return new ValidationResult(ErrorMessage ?? $"Age must be between {MinimumAge} and {MaxAge}.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
--!>*/


public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _min;
    public int MaxAge { get; set; }

    public MinimumAgeAttribute(int min) => _min = min;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dob)
        {
            var age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age)) age--;

            if (age >= _min && age <= MaxAge)
                return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}

}
