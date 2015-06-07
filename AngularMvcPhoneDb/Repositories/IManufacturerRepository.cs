using System.Collections.Generic;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.DomainDto;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public interface IManufacturerRepository
    {
        /// <summary>
        /// Load all phone manufacturers with their phone products
        /// </summary>
        IEnumerable<PhoneManufacturerDto> LoadAllWithPhones();

        bool Any(string manu);
        Manufacturer LoadById(string manu);
        void Save(Manufacturer manufacturer);
    }
}