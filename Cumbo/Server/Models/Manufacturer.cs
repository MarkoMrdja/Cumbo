using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<ProductModel>? ProductModels { get; set; }
    }
}
