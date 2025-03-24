using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Dws.Note_one.Api.Domain.Models; 
using Dws.Note_one.Api.Services; 
using Dws.Note_one.Api.Resource; 
using System.Collections.Generic;
using System.Threading.Tasks;
using Dws.Note_one.Api.Extension;

namespace Dws.Note_one.Api.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return resources;
        }
        [HttpGet("bycategory/{categoryName}")]
        public async Task<IEnumerable<ProductResource>> GetByCategoryNameAsync(string categoryName)
        {
            var products = await _productService.ListByCategoryNameAsync(categoryName);
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.SaveAsync(product, resource.CategoryName);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            return Ok(productResource);
        }
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message); 
            }

            return NoContent(); 
        }
    }
}