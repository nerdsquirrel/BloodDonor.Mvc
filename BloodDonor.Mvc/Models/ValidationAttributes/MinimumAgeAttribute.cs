using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.ValidationAttributes
{
    public class MinimumAgeAttribute: ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge = 18)
        {
            _minimumAge = minimumAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                var age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;
                
                if (age < _minimumAge)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old to donate blood.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
