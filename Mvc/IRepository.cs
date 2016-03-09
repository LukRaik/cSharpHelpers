using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_hiber_app.Interface
{
    interface IRepository<T>
    {
        T FindById(int id);
        List<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
        void Insert(T entity);
    }
}


select distinct(s1.name) from scores s1 inner join scores s2 on s1.id=s2.id