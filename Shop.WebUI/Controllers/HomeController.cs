using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        SQLRepository<Product> context;
        SQLRepository<ProductCategory> contextCategory;

        public HomeController()
        {
            context = new SQLRepository<Product>(new MyContext());
            contextCategory = new SQLRepository<ProductCategory>(new MyContext());
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = contextCategory.Collection().ToList();
            if(Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }
            ProductListViewModel productListView = new ProductListViewModel();
            productListView.Products = products;
            productListView.ProductCategories = productCategories;
            return View(productListView);
        }

        public ActionResult Details(int Id)
        {
            Product product = context.FindById(Id);
            if (product==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}