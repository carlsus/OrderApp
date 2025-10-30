using OrderApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OrderApp.Helper
{
    public class UniqueMobileNumberAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var db = validationContext.GetService(typeof(OrderDBContext)) as OrderDBContext;
            if (db == null)
            {
                throw new InvalidOperationException("OrderDBContext service is not available.");
            }
            var name = value?.ToString();
            var user = (Customer)validationContext.ObjectInstance;

            var existing = db.Customers.FirstOrDefault(u =>
            u.MobileNumber == user.MobileNumber &&
            u.IsActive == user.IsActive);

            if (existing != null && existing.Id != user.Id)
            {
                var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                    ? "A customer with the same Mobile Number already exists."
                    : ErrorMessage;

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
