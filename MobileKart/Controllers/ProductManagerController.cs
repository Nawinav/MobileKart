using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileKart.Core;
using MobileKart.Data.InMemory;
using MobileKart.Service;
using MobileKart.Data.SQL;

namespace MobileKart.Controllers
{
    public class ProductManagerController : Controller
    {
        // GET: Product
        ProductRepository Context;
        public ProductManagerController()
        {
            Context = new ProductRepository();
        }
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                Context.Insert(product);
                Context.commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }

        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = Context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product); 
                }
                else
                {
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;
                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;

                    Context.commit();

                    return RedirectToAction("Index");

                }
                
            }

        }
        public ActionResult Delete(string Id)
        {
            Product product = Context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();     
            }
            else
            {
                return View(product); 
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = Context.Find(Id);

            if (productToDelete == null)
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