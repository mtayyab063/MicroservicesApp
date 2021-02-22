using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Product> GetProduct(string productId)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == productId)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string productCategory)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, productCategory);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, productName);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
