namespace OrderApp.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int Skuid { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime Timestamp { get; set; }

        public string UserId { get; set; } = string.Empty;

        public SKU Sku { get; set; } = null!;
    }
}
