using Decorator.Business.Service;
using Decorator.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Decorator.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetProductByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products product)
        {
            return Ok(await _productService.CreateProductAsync(product));
        }

    }
}