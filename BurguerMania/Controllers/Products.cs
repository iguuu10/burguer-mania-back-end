using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.Product;
using BurguerManiaAPI.Dto.Product;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductInterface _productService;

        // Construtor para injeção de dependência
        public ProductsController(ProductInterface productService)
        {
            _productService = productService;
        }

        // Obter todos os produtos
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetProducts();
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Obter um produto específico pelo ID
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProduct(id);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Atualizar um produto existente pelo ID
        [HttpPut("PutProducts/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
        {
            var result = await _productService.PutProducts(id, productRequest);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Criar um novo produto
        [HttpPost("PostProducts")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            var result = await _productService.PostProducts(productRequest);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        // Deletar um produto pelo ID
        [HttpDelete("delete/product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProducts(id);
            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }
    }
}
