using MobileKart.Core.Models;
using MobileKart.Data.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileKart.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager
        ProductCategoryRepository Context;
        public ProductCategoryManagerController()
        {
            Context = new ProductCategoryRepository();
        }
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = Context.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                Context.Insert(productCategory);
                Context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = Context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }

        }
        [HttpPost]
        public ActionResult Edit(string Id,ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = Context.Find(Id);
            if (productCategoryToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                productCategoryToUpdate.Category = productCategory.Category;
                Context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategory = Context.Find(Id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoriesToDelete = Context.Find(Id);

            if (productCategoriesToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.commit();
                return RedirectToAction("Index");
            }
        }

    }
}