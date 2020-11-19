using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryController()
        {
            context = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productscat = context.Collection().ToList();
            return View(productscat);
        }

        public ActionResult Create()
        {
            ProductCategory pcat = new ProductCategory();
            return View(pcat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory pcat)
        {
            if (!ModelState.IsValid)
            {
                return View(pcat);
            }
            else
            {
                context.Insert(pcat);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory pcat = context.FindById(id);
                if (pcat == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(pcat);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory pcat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(pcat);
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
                return View(pcat.Id);
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory pcat = context.FindById(id);
                if (pcat == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(pcat);
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
