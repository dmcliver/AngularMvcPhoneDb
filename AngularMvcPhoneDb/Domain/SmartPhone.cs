using System;

namespace AngularMvcPhoneDb.Core.Domain
{
    public class SmartPhone
    {
        public Guid SmartPhoneId { get; set; }
        public String Model { get; set; }
        public int BatteryCapacity { get; set; }
        public string ManufacturerName { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int PixelWidth { get; set; }
        public int PixelHeight { get; set; }
        public String Cpu { get; set; }
        public String Gpu { get; set; }
        public DisplayType DisplayType { get; set; }
        public DateTime ModelYear { get; set; }
    }
}

