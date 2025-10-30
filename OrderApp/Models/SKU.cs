using OrderApp.Helper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderApp.Models
{
    public class SKU
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [UniqueSKUName]
        public string Name { get; set; } 

        [Required(ErrorMessage = "Code is required.")]
        [UniqueSKUCode]
        public string Code { get; set; } 

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit Price must be a positive value.")]
        public decimal UnitPrice { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = Environment.UserName;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string UserId { get; set; } = Environment.UserName;
        public bool IsActive { get; set; }

        public string? ImagePath { get; set; }
    }
}
