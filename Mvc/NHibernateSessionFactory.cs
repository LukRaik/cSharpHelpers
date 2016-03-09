using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_hiber_app.UnitOfWork
{
    public class NHibernateSessionFactory
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory Get
        {
            get
            {
                if(_sessionFactory==null)
                {
                    _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(
                @"Server=localhost\SQLEXPRESS;Database=Nhibernate;Trusted_Connection=true;")
                )
                .Mappings(m =>
                m.FluentMappings
                .AddFromAssemblyOf<Models.Person>().AddFromAssemblyOf<Models.Safe>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false,true))
                .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
    }
}
