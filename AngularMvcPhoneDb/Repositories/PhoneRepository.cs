using System;
using AngularMvcPhoneDb.Core.Domain;
using NHibernate;
using NHibernate.Infrastructure;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IHibernateSessionManager _sessionManager;

        public PhoneRepository(IHibernateSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public PhoneRepository() : this(new HibernateSessionManager()) {}

        public Guid Add(SmartPhone phone)
        {
            ISession session = _sessionManager.CreateSession();
            session.Save(phone);
            session.Flush();
            return phone.SmartPhoneId;
        }

        public SmartPhone Create()
        {
            return new SmartPhone();
        }

        public SmartPhone LoadById(Guid id)
        {
            SmartPhone phone = _sessionManager.CreateSession().Get<SmartPhone>(id);
            return phone;
        }
    }
}
