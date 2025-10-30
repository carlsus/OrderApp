using OrderApp.Helper;

namespace OrderApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [UniqueName]
        public string FullName => $"{FirstName} {LastName}";
        [UniqueMobileNumber]
        public long MobileNumber { get; set; }
        public string City { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public string CreatedBy { get; set; }= Environment.UserName;
        public DateTime Timestamp { get; set; }=DateTime.Now;
        public string UserId { get; set; } = Environment.UserName;
        public bool IsActive { get; set; }

       
    }
}
