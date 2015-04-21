using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using AngularMvcPhoneDb.Core.Domain;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        public void Add(SmartPhone phone)
        {
            using (var dbContext = new PhoneDbContext())
            {
                    dbContext.SmartPhone.Add(phone);
                    dbContext.Entry(phone).State = EntityState.Added;
                    dbContext.SaveChanges();
            }
        }

        public SmartPhone Create()
        {
            using (var dbContext = new PhoneDbContext())
            {
                return dbContext.SmartPhone.Create();
            }
        }

        public SmartPhone LoadById(Guid id)
        {
            using (var dbContext = new PhoneDbContext())
            {
                return dbContext.SmartPhone.Single(p => p.SmartPhoneId.Equals(id));
            }
        }
    }
}
