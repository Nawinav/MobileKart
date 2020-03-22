using MobileKart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MobileKart.Data.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> products; //assigning model properties to products

        public ProductCategoryRepository()
        {
            products = cache["productCategories"] as List<ProductCategory>;
            if (products == null)
            {
                products = new List<ProductCategory>();//now only creating model properties 
            }
        }
        public void commit()
        {
            cache["productCategories"] = products;
        }
        public void Insert(ProductCategory p)
        {
            products.Add(p);

        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productToUpdate = products.Find(p => p.Id == productCategory.Id);
            if (productToUpdate != null)
            {
                productToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}


