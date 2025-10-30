using OrderApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OrderApp.Helper
{
    public class UniqueSKUNameAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var db = validationContext.GetService(typeof(OrderDBContext)) as OrderDBContext;
            if (db == null)
            {
                throw new InvalidOperationException("OrderDBContext service is not available.");
            }
            var name = value?.ToString();
            var sku = (SKU)validationContext.ObjectInstance;

            var existing = db.Skus.FirstOrDefault(u =>
            u.Name == sku.Name  && u.IsActive == true);

            if (existing != null && existing.Id != sku.Id)
            {
                var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                    ? "SKU name already exists."
                    : ErrorMessage;

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
