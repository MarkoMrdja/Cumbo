using System.Reflection.Metadata.Ecma335;

namespace Cumbo.Server.Models
{
    public class Phone : Product
    {
        public int Memory { get; set; }
        public string Color { get; set; }
        public int BatteryLife { get; set; }
        public override string Type => typeof(Phone).Name;
    }
}
