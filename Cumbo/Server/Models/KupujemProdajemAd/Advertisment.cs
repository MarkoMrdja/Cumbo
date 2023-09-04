using Cumbo.Shared.Enums;

namespace Cumbo.Server.Models.KupujemProdajemAd
{
    public class Advertisment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Url { get; set; }
        public ProductType ProductType { get; set; }
        public long? IMEI { get; set; }
        public bool CurrentlyActive { get; set; }
        public DateTime LastActive { get; set; }
    }
}
