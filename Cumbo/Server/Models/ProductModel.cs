using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        [JsonIgnore]
        public Manufacturer Manufacturer { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
