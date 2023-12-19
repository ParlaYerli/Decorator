using Decorator.Data.Models;
using Decorator.DataAccess.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Products> CreateProductAsync(Products product)
        {
            await _productRepository.CreateProduct(product);

            return product;
        }

        public async Task<Products> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task<List<Products>> GetProducts()
        {
            var list = await _productRepository.GetAll();
            return list;
        }
    }
}
