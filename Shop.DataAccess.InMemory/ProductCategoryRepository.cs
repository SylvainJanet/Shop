using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productcategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productcategories"] = productCategories;
        }

        public void Insert(ProductCategory pcat)
        {
            productCategories.Add(pcat);
        }

        public void Update(ProductCategory pcat)
        {
            ProductCategory dbproductcat = productCategories.Find(productcat => pcat.Id == productcat.Id);
            if (dbproductcat != null)
            {
                dbproductcat.Category = pcat.Category;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory FindById(int id)
        {
            ProductCategory pcat = productCategories.SingleOrDefault(productcat => id == productcat.Id);
            if (pcat != null)
            {
                return pcat;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory pcat = productCategories.SingleOrDefault(productcat => id == productcat.Id);
            if (pcat != null)
            {
                productCategories.Remove(pcat);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
