using System.Text.Json.Serialization;

namespace Cumbo.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [JsonIgnore]
        public List<Transaction>? Transactions { get; set; }
    }
}
