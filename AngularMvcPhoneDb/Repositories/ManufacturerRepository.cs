using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.DomainDto;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        /// <summary>
        /// Load all phone manufacturers with their phone products
        /// </summary>
        public IEnumerable<PhoneManufacturerDto> LoadAllWithPhones()
        {
            using (var dbContext = new PhoneDbContext())
            {
                return
                    dbContext.Manufacturer.Include(m => m.SmartPhones).ToList().Select
                    (
                        m =>
                            new PhoneManufacturerDto
                            (
                                m.Name,
                                m.SmartPhones.Select(p => new PhoneSummaryDto(p.SmartPhoneId, p.Model)).OrderBy(pd => pd.ModelName)
                             )
                       );
            }
        }

        public bool Any(string manu)
        {
            using (var dbContext = new PhoneDbContext())
            {
                return dbContext.Manufacturer.Any(m => m.Name.ToLower() == manu.ToLower());    
            }
        }

        public Manufacturer LoadById(string manu)
        {
            using (var dbContext = new PhoneDbContext())
            {
                return dbContext.Manufacturer.Single(m => m.Name.ToLower() == manu.ToLower());
            }
        }
    }
}
