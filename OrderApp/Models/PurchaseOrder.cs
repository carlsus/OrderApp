using System.ComponentModel.DataAnnotations;

namespace OrderApp.Models
{

    public enum StatusType
    {
        [Display(Name = "New")]
        New = 1,

        [Display(Name = "Completed")]
        Completed = 2,

        [Display(Name = "Cancelled")]
        Cancelled = 3
    }
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public Customer Customer { get; set; } 

     
        public DateOnly DateOfDelivery { get; set; }

      
        public StatusType status { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Amount due must be a positive value.")]
        public decimal AmountDue { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = Environment.UserName;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string UserId { get; set; } = Environment.UserName;
        public bool IsActive { get; set; }

        public ICollection<PurchaseItem> Items { get; set; } 
    }
}
