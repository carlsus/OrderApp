using Microsoft.EntityFrameworkCore;
using OrderApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OrderApp.Helper
{
    public class UniqueNameAttribute : ValidationAttribute
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
            u.FirstName == user.FirstName &&
            u.LastName == user.LastName && u.IsActive==true);

            if (existing != null && existing.Id != user.Id)
            {
                var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                    ? "A user with the same first and last name already exists."
                    : ErrorMessage;

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
