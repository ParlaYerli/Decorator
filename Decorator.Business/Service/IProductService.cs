using Decorator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.Business.Service
{
    public interface IProductService
    {
        Task<List<Products>> GetProducts();

        Task<Products> GetProductByIdAsync(int id);
        Task<Products> CreateProductAsync(Products product);
    }
}
