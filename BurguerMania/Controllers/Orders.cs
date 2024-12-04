using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.Order;
using BurguerManiaAPI.Dto.Order;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderInterface _orderService;

        /// <summary>
        /// Inicializa o controlador de Orders com a interface de serviço injetada.
        /// </summary>
        /// <param name="orderService">Interface responsável pelas operações de Orders.</param>
        public OrdersController(OrderInterface orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retorna todos os pedidos registrados.
        /// </summary>
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetOrders();

            if (!response.Status && response.StatusCode == 404)
                return NotFound(new { status = 404, errors = response.Mensagem });

            return Ok(response);
        }

        /// <summary>
        /// Busca um pedido específico pelo ID.
        /// </summary>
        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrder(id);

            if (!response.Status && response.StatusCode == 404)
                return NotFound(new { status = 404, errors = response.Mensagem });

            return Ok(response);
        }

        /// <summary>
        /// Atualiza os dados de um pedido existente.
        /// </summary>
        [HttpPut("PutOrders/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderRequest orderRequest)
        {
            var response = await _orderService.PutOrders(id, orderRequest);

            if (!response.Status && response.StatusCode == 404)
                return NotFound(new { status = 404, errors = response.Mensagem });

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        [HttpPost("PostOrders")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var response = await _orderService.PostOrders(orderRequest);

            if (!response.Status && response.StatusCode == 404)
                return NotFound(new { status = 404, errors = response.Mensagem });

            return Ok(response);
        }

        /// <summary>
        /// Remove um pedido pelo ID.
        /// </summary>
        [HttpDelete("DeleteOrders/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _orderService.DeleteOrders(id);

            if (!response.Status && response.StatusCode == 404)
                return NotFound(new { status = 404, errors = response.Mensagem });

            return Ok(response);
        }
    }
}
