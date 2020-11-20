using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory;

        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            contextCategory = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = contextCategory.Collection().ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductManagerViewModel pvm)
        {
            if (!ModelState.IsValid)
            {
                return View(pvm);
            }
            else
            {
                context.Insert(pvm.Product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = context.FindById(id);
                viewModel.ProductCategories = contextCategory.Collection().ToList(); 
                if (viewModel.Product == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductManagerViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(pvm.Product);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }

            }
            else
            {
                return View(pvm.Product.Id);
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Delete(id);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }

            }
            else
            {
                return View(id);
            }

        }
    }
}