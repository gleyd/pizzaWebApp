using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPizza.Models
{
    public interface IRepositoryable<T>
        where T : class
    {
        void Create(T obj);
        void Update(T obj);
        void Delete(int key);

        T ReadById(int key);

        List<T> Read(Func<T, bool> whereClause);
        List<T> Read(int take, int skip);
    }
}
