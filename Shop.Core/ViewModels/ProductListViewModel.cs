using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
