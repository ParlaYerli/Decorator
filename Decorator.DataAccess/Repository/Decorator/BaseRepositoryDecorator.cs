using Decorator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.DataAccess.Repository.Decorator
{
    public class BaseRepositoryDecorator : IProductRepository
    {
        public readonly IProductRepository _productRepository;
        
        public BaseRepositoryDecorator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async virtual Task<Products> CreateProduct(Products model)
        {
            return await _productRepository.CreateProduct(model);
        }
        public async virtual Task DeleteProduct(int id)
        {
             await _productRepository.DeleteProduct(id);
        }
        public async virtual Task<List<Products>> GetAll()
        {
            return await _productRepository.GetAll();
        }
        public async virtual Task<Products> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async virtual Task UpdateProduct(Products model)
        {
            await _productRepository.UpdateProduct(model);
        }
    }
}
