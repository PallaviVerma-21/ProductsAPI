using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsAPI.Model;
using ProductsAPI.Data;
namespace ProductsAPI.Services
{
    public interface IProduct
    {
        //CRUD Operations
        IEnumerable<Product> GetProducts();

        Product GetProduct(int id);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);
        
    }
}
