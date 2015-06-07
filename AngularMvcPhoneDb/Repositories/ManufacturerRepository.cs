using System;
using System.Collections.Generic;
using System.Linq;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.DomainDto;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Infrastructure;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IHibernateSessionManager session_Manager;

        public ManufacturerRepository(IHibernateSessionManager sessionManager)
        {
            session_Manager = sessionManager;
        }

        public ManufacturerRepository() : this(new HibernateSessionManager()) {}
         
        /// <summary>
        /// Load all phone manufacturers with their phone products
        /// </summary>
        public IEnumerable<PhoneManufacturerDto> LoadAllWithPhones()
        {
            ISession session = session_Manager.CreateSession();
            IList<SmartPhone> smartphones = session.CreateCriteria<SmartPhone>()
                                                   .List<SmartPhone>();

            if(!smartphones.Any())
                return new List<PhoneManufacturerDto>();

            return smartphones.GroupBy(p => p.Manufacturer)
                              .Select(x => new PhoneManufacturerDto
                                           (
                                                x.Key.Name,
                                                x.Select(p => new PhoneSummaryDto
                                                              (
                                                                p.SmartPhoneId, 
                                                                p.Model
                                                              )
                                                        ).OrderBy(psd => psd.ModelName)
                                            )
                                     );
        }

        public bool Any(string manu)
        {
            IProjection mfName = Projections.Property<Manufacturer>(x => x.Name);

            int? count = session_Manager.CreateSession()
                                        .CreateCriteria<Manufacturer>()
                                        .SetProjection(Projections.Count(Projections.Id()))
                                        .Add(Restrictions.Eq(mfName , manu.ToLower()).IgnoreCase())
                                        .UniqueResult() as int?;

            return count > 0;
        }

        public Manufacturer LoadById(string manu)
        {
            IProjection mfName = Projections.Property<Manufacturer>(x => x.Name);

            Manufacturer m = session_Manager.CreateSession()
                                            .CreateCriteria<Manufacturer>()
                                            .Add(Restrictions.Eq(mfName, manu.ToLower()).IgnoreCase())
                                            .UniqueResult() as Manufacturer;

            return m;
        }

        public void Save(Manufacturer manufacturer)
        {
            ISession session = session_Manager.CreateSession();
            session.Save(manufacturer);
            session.Flush();
        }
    }
}
