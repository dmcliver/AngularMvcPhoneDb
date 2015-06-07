using _Type = System.Type;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Context;

namespace NHibernate.Infrastructure
{
    public class HibernateSessionManager : IHibernateSessionManager
    {
        private static ISessionFactory sessionFactory_;
        private static IPersistenceConfigurer dbType_ = MsSqlConfiguration.MsSql2012.ConnectionString(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true");
        private static _Type mappingAssemblyType_ = typeof(HibernateSessionManager);
        private static Hbm2Ddl hbm2Ddl_ = Hbm2Ddl.None;

        /// <summary>
        /// Creates the session.
        /// </summary>
        public ISession CreateSession()
        {
            if (sessionFactory_ == null)
            {
                sessionFactory_ = Fluently.Configure()
                                          .Database(dbType_)
                                          .Mappings(c => c.FluentMappings.AddFromAssembly(Assembly.GetAssembly(mappingAssemblyType_)))
                                          .ExposeConfiguration(c => SetProperties(c))
                                          .BuildSessionFactory();
            }

            if (CurrentSessionContext.HasBind(sessionFactory_))
                return sessionFactory_.GetCurrentSession();

            ISession session = sessionFactory_.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        private static Configuration SetProperties(Configuration config)
        {
            config.SetProperty("current_session_context_class", "web");
            config.SetProperty("hbm2ddl.auto", hbm2Ddl_.ToString());
            return config;
        }

        /// <summary>
        /// Closes the session and unbinds it from the factories current context.
        /// </summary>
        public static void Close()
        {
            bool hasSessionFactory = sessionFactory_ != null && !sessionFactory_.IsClosed;
            if (hasSessionFactory && CurrentSessionContext.HasBind(sessionFactory_))
            {
                ISession session = CurrentSessionContext.Unbind(sessionFactory_);
                session.Close();
                session.Dispose();
            }
        }

        /// <summary>
        /// Closes the session and factory
        /// </summary>
        public static void CloseAll()
        {
            Close();
            if (sessionFactory_ != null && !sessionFactory_.IsClosed)
            {
                sessionFactory_.Close();
                sessionFactory_.Dispose();
            }
        }

        /// <summary>
        /// Configuration method for nhibernate.
        /// </summary>
        /// <param name="dbType">Type of the db and connection configuration.</param>
        /// <param name="mappingAssemblyType">Type of the mapping assembly.</param>
        /// <param name="hbm2Ddl">The hibernate db operation to perform, default performs no operation on db</param>
        public static void Configure(IPersistenceConfigurer dbType, System.Type mappingAssemblyType, Hbm2Ddl hbm2Ddl = null) 
        {
            dbType_ = dbType ?? dbType_;
            mappingAssemblyType_ = mappingAssemblyType ?? mappingAssemblyType_;
            hbm2Ddl_ = hbm2Ddl ?? hbm2Ddl_;
        }
    }
}