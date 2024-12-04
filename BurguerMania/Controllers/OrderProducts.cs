using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.OrderProduct;
using BurguerManiaAPI.Dto.OrderProduct;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrderProductInterface _service;

        
        /// Construtor do controlador de OrderProducts.
        
        /// <param name="service">Interface responsável pelas operações relacionadas aos OrderProducts.</param>
        public OrderProductsController(IOrderProductInterface service)
        {
            _service = service;
        }

        
        /// Retorna todos os OrderProducts cadastrados.
        
        [HttpGet("GetOrderProducts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetOrderProducts();

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        
        /// Retorna um OrderProduct específico pelo ID.
        
        [HttpGet("GetOrderProduct/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetOrderProduct(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

       
        /// Atualiza os dados de um OrderProduct existente.
        
        [HttpPut("PutOrderProducts/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] OrderProductRequest request)
        {
            var result = await _service.PutOrderProducts(id, request);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        
        /// Adiciona um novo OrderProduct.
       
        [HttpPost("PostOrderProducts")]
        public async Task<IActionResult> CreateAsync([FromBody] OrderProductRequest request)
        {
            var result = await _service.PostOrderProducts(request);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        
        /// Remove um OrderProduct pelo ID.
        
        [HttpDelete("DeleteOrderProducts/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteOrderProducts(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }
    }
}
