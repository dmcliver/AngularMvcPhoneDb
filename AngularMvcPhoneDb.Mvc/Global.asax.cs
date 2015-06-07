using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AngularMvcPhoneDb.Core.HibernateMappings;
using FluentNHibernate.Cfg.Db;
using NHibernate.Infrastructure;

namespace AngularMvcPhoneDb.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PhoneDb"].ConnectionString;
            HibernateSessionManager.Configure(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString), typeof(ManufacturerMapping), Hbm2Ddl.Create);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest(object sender, EventArgs evt)
        {
            HibernateSessionManager.Close();
        }

        protected void Application_End()
        {
            HibernateSessionManager.CloseAll();
        }
    }
}