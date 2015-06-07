using System;
using AngularMvcPhoneDb.Core.Domain;
using FluentNHibernate.Mapping;

namespace AngularMvcPhoneDb.Core.HibernateMappings
{
    public class SmartPhoneMapping : ClassMap<SmartPhone>
    {
        public SmartPhoneMapping()
        {
            Table("SmartPhone");
            Id(sp => sp.SmartPhoneId);            
            Map(sp => sp.Cpu).Not.Nullable();
            Map(sp => sp.Gpu).Not.Nullable();
            Map(sp => sp.DisplayType).CustomType<int>().Not.Nullable();
            Map(sp => sp.BatteryCapacity);
            Map(sp => sp.Model).Not.Nullable();
            Map(sp => sp.ModelYear);
            Map(sp => sp.PixelWidth).Not.Nullable();
            Map(sp => sp.PixelHeight).Not.Nullable();
            References(sp => sp.Manufacturer).Column("ManufacturerName");
        }
    }
}
