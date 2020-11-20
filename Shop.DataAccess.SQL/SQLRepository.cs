using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal MyContext DataContext;
        internal DbSet<T> dbSet;

        public SQLRepository(MyContext dataContext)
        {
            DataContext = dataContext;
            dbSet = DataContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T item = FindById(id);
            if(DataContext.Entry(item).State==EntityState.Detached)
            {
                dbSet.Attach(item);
            }
            dbSet.Remove(item);
        }

        public T FindById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            DataContext.Entry(item).State = EntityState.Modified;
        }
    }
}
