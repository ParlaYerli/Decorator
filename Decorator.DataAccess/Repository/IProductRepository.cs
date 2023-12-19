using Decorator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decorator.DataAccess.Repository
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll();
        Task<Products> GetByIdAsync(int id);
        Task<Products> CreateProduct(Products model);
        Task UpdateProduct(Products model);
        Task DeleteProduct(int id);
    }
}
