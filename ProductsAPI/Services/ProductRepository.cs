using ProductsAPI.Data;
using ProductsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Services
{
    //1. Implementing the IProduct interface 
    public class ProductRepository : IProduct
    {
        private ProductDbContext productDbContext;

        public ProductRepository(ProductDbContext _productDbContext)
        {
            productDbContext = _productDbContext;
        }

        void IProduct.AddProduct(Product product)
        {
            productDbContext.Products.Add(product);
            productDbContext.SaveChanges(true);
            
        }

        void IProduct.DeleteProduct(int id)
        {
            var product = productDbContext.Products.FirstOrDefault(p => p.Id == id);
            productDbContext.Products.Remove(product);
            productDbContext.SaveChanges(true);            
        }

        Product IProduct.GetProduct(int id)
        {
            var product = productDbContext.Products.SingleOrDefault(p => p.Id == id);
            if(product == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return product;
        }

        IEnumerable<Product> IProduct.GetProducts()
        {
            return productDbContext.Products;
        }

        void IProduct.UpdateProduct(Product product)
        {

            productDbContext.Products.Update(product);
            productDbContext.SaveChanges(true);
            
        }
    }
}
