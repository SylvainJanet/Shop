using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal ObjectCache cache = MemoryCache.Default;
        internal List<T> items;
        internal string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T item)
        {
            items.Add(item);
        }

        public void Update(T item)
        {
            T dbitem = items.Find(i => item.Id == i.Id);
            if (dbitem != null)
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.Name != "Id")
                    {
                        var property = typeof(T).GetProperty(prop.Name);
                        property.SetValue(dbitem, Convert.ChangeType(property.GetValue(item), prop.PropertyType));
                    }
                }
            }
            else
            {
                throw new Exception("Item of class " + className + " not found");
            }
        }

        public T FindById(int id)
        {
            T item = items.SingleOrDefault(i => id == i.Id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Item of class " + className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(int id)
        {
            T item = items.SingleOrDefault(i => id == i.Id);
            if (item != null)
            {
                items.Remove(item);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
