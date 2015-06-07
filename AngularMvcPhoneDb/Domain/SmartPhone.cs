using System;

namespace AngularMvcPhoneDb.Core.Domain
{
    public class SmartPhone
    {
        public virtual Guid SmartPhoneId { get; set; }
        public virtual String Model { get; set; }
        public virtual int BatteryCapacity { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual int PixelWidth { get; set; }
        public virtual int PixelHeight { get; set; }
        public virtual String Cpu { get; set; }
        public virtual String Gpu { get; set; }
        public virtual DisplayType DisplayType { get; set; }
        public virtual DateTime ModelYear { get; set; }
    }
}

