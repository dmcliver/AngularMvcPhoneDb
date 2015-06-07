using AngularMvcPhoneDb.Core.Domain;
using FluentNHibernate.Mapping;

namespace AngularMvcPhoneDb.Core.HibernateMappings
{
    public class ManufacturerMapping : ClassMap<Manufacturer>
    {
        public ManufacturerMapping()
        {
            Table("Manufacturer");
            Id(m => m.Name);
        }
    }
}
