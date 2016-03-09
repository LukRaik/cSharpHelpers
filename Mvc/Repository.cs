using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_hiber_app.Repository
{
    public class Repository<T> : Interface.IRepository<T>
    {
        private UnitOfWork.UoW _uow;

        public Repository(UnitOfWork.UoW uow)
        {
            _uow = uow;
        }

        public T FindById(int id)
        {
            _uow.OpenSession();
            return _uow.Session.Get<T>(id);
        }

        public List<T> GetAll()
        {
            _uow.OpenSession();
            return _uow.Session.CreateCriteria(typeof(T)).List<T>().ToList();
        }

        public void Update(T entity)
        {
            _uow.OpenSession();
            _uow.BeginTransaction();
            _uow.Session.Update(entity);
        }

        public void Delete(T entity)
        {
            _uow.OpenSession();
            _uow.BeginTransaction();
            _uow.Session.Delete(entity);
        }

        public void Insert(T entity)
        {
            _uow.OpenSession();
            _uow.BeginTransaction();
            _uow.Session.Save(entity);
        }
    }
}
