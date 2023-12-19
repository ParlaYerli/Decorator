using Decorator.Data.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.DataAccess.Repository.Decorator.Concrete
{
    public class LoggingDecoratorRepository : BaseRepositoryDecorator
    {
        private readonly ILogger<LoggingDecoratorRepository> _logger;
        public LoggingDecoratorRepository(IProductRepository productRepository, ILogger<LoggingDecoratorRepository> logger) : base(productRepository)
        {
            _logger = logger;
        }
        public override async Task<List<Products>> GetAll()
        {
            _logger.LogInformation("GetAll() methodu calıstı...");
            var allProducts = await base.GetAll();
            return allProducts;
        }
        public override async Task<Products> CreateProduct(Products product)
        {
            await base.CreateProduct(product);
            _logger.LogInformation("CreateProduct() methodu calıstı...");
            return product;
        }
        public override async Task UpdateProduct(Products model)
        {
            _logger.LogInformation("UpdateProduct() methodu calıstı...");
            await base.UpdateProduct(model);
        }
        public override async Task DeleteProduct(int id)
        {
            _logger.LogInformation("DeleteProduct() methodu calıstı...");
            await base.DeleteProduct(id);
        }
    }
}
