using Bookworm.DTO;
using Bookworm.Dtos.Request;
using Bookworm.Dtos.Response;
using Bookworm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    [EnableCors("AllowReactApp")] // You would define this policy in Program.cs
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            List<ProductResponseDto> products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById([FromRoute] int id)
        {
            ProductResponseDto product = await _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] ProductRequestDto productRequestDto)
        {
            ProductResponseDto createdProduct = await _productService.CreateProduct(productRequestDto);
            return StatusCode(StatusCodes.Status201Created, createdProduct);
        }

        [HttpPut("{id}")]
       
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromRoute] int id, [FromBody] ProductRequestDto productRequestDto)
        {
            ProductResponseDto updatedProduct = await _productService.UpdateProduct(id, productRequestDto);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
 
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("search/by-author")]
        public async Task<ActionResult<List<ProductResponseDto>>> FindProductsByAuthor([FromQuery(Name = "author")] string authorName)
        {
            List<ProductResponseDto> products = await _productService.FindProductsByAuthor(authorName);
            return Ok(products);
        }

        [HttpGet("category/{genreId}")]
        public async Task<ActionResult<List<ProductResponseDto>>> FindProductsByCategory([FromRoute] int genreId)
        {
            List<ProductResponseDto> products = await _productService.FindProductsByGenre(genreId);
            return Ok(products);
        }

        [HttpGet("search/by-name")]
        public async Task<ActionResult<List<ProductResponseDto>>> FindProductsByName([FromQuery(Name = "name")] string productName)
        {
            List<ProductResponseDto> products = await _productService.FindProductsByName(productName);
            return Ok(products);
        }

        [HttpGet("language/{languageId}")]
        public async Task<ActionResult<List<ProductResponseDto>>> FindProductsByLanguage([FromRoute] int languageId)
        {
            List<ProductResponseDto> products = await _productService.FindProductsByLanguage(languageId);
            return Ok(products);
        }
    }
}