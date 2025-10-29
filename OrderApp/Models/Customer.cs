namespace OrderApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MobileNumber { get; set; }
        public string City { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Timesamp { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }

    }
}
