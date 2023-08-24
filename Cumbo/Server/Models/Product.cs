using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public int ProductModelId { get; set; }
        public decimal BoughtFor { get; set; }
        public abstract string Type { get; }
        public DateTime DateOfBuying { get; set; }
        [JsonIgnore]
        public ProductModel ProductModel { get; set; }
        [JsonIgnore]
        public List<Transaction> Transactions { get; set; }
    }
}
