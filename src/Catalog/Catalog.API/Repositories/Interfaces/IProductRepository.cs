using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string productId);
        Task<IEnumerable<Product>> GetProductsByName(string productName);
        Task<IEnumerable<Product>> GetProductsByCategory(string productCategory);
        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);

    }
}
