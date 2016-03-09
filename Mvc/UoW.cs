using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_hiber_app.Models;
using Test_hiber_app.Interface;
using Test_hiber_app.Repository;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using System.Data;

namespace Test_hiber_app.UnitOfWork
{
    public class UoW:IUoW
    {
        private ISession _session { get; set; }
        private ITransaction _transaction { get; set; }
        public ISession Session {
           get
            {
                return _session;
            }
        }


        public Repository<Person> personrepo { get;private set; }
        public Repository<Safe> saferepo { get;private set; }
        public Repository<Content> contentrepo { get;private set; }

        public UoW()
        {
            personrepo = new Repository<Person>(this);
            saferepo = new Repository<Safe>(this);
            contentrepo = new Repository<Content>(this);
        }

        public ISession OpenSession()
        {
            if(_session==null||!_session.IsConnected)
            {
                if (_session != null) _session.Dispose();
                _session = NHibernateSessionFactory.Get.OpenSession();
            }
            return _session;
        }
        public ITransaction BeginTransaction()
        {
            if(_transaction==null||!_transaction.IsActive)
            {
                if (_transaction != null) _transaction.Dispose();
                try
                {
                    _transaction = _session.BeginTransaction();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return _transaction;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Dispose()
        {
            if(_session!=null)_session.Dispose();
            if (_transaction != null) _transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
