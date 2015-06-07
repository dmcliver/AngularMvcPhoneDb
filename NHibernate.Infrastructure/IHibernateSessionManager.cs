namespace NHibernate.Infrastructure
{
    public interface IHibernateSessionManager
    {
        ISession CreateSession();
    }
}