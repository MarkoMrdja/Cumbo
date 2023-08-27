using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeOfTransaction { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
