using Decorator.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.DataAccess.Repository.Decorator.Concrete
{
    public class CacheDecoratorRepository : BaseRepositoryDecorator
    {
        private const string cacheName = "products";
        private readonly IMemoryCache _memoryCache;
        public CacheDecoratorRepository(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
        {
            _memoryCache = memoryCache;
        }
        public override async Task<List<Products>> GetAll()
        {
            if (_memoryCache.TryGetValue(cacheName, out List<Products> cacheProducts))
            {
                return cacheProducts;
            }

            var allProducts = await base.GetAll();
            _memoryCache.Set(cacheName, allProducts);
            return allProducts;
        }
        public override async Task<Products> CreateProduct(Products product)
        {
            await base.CreateProduct(product);
            await CacheUpdateData();
            return product;
        }
        public override async Task UpdateProduct(Products model)
        {
            await base.UpdateProduct(model);
            await CacheUpdateData();
        }
        public override async Task DeleteProduct(int id)
        {
            await base.DeleteProduct(id);
            await CacheUpdateData();
        }
        private async Task CacheUpdateData()
        {
            _memoryCache.Remove(cacheName);
            _memoryCache.Set(cacheName, await base.GetAll());
        }
    }
}

