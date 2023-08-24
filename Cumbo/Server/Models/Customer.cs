using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public List<Transaction> Transactions { get; set; }
    }
}
