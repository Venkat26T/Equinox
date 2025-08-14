using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Equinox.Models.DataLayer;

public class UniquePhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var db = validationContext.GetService(typeof(EquinoxContext)) as EquinoxContext;

        if (value is not string phone || db == null)
            return new ValidationResult("Phone number is required.");

        bool exists = db.Users.Any(u => u.PhoneNumber == phone);

        return exists
            ? new ValidationResult(ErrorMessage ?? "Phone number already exists.")
            : ValidationResult.Success;
    }
}
