using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Transaction>? Transactions { get; set; }
    }
}
