using BloodDonor.Mvc.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.ValidationAttributes
{
    public class UniqueEmailAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email && !string.IsNullOrWhiteSpace(email))
            {
                var donorService = validationContext.GetService<IBloodDonorService>();
                if (donorService is null)
                {
                    return ValidationResult.Success;
                }
                var existingDonors = donorService.GetAllAsync().GetAwaiter().GetResult();

                if (existingDonors.Any(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
