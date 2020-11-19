using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository(ObjectCache cache)
        {
            products = cache["products"] as List<Product>;
            if (products==null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product dbproduct = products.SingleOrDefault(product => p.Id == product.Id);
            if (dbproduct!=null)
            {
                dbproduct = p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Product FindById(int id)
        {
            Product p = products.SingleOrDefault(product => id == product.Id);
            if (p!=null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(int id)
        {
            Product p = products.SingleOrDefault(product => id == product.Id);
            if (p !=null)
            {
                products.Remove(p);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
