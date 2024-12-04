using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurguerManiaAPI.Interfaces.UserOrder;
using BurguerManiaAPI.Dto.UserOrder;

namespace BurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrdersController : ControllerBase
    {
        private readonly UserOrderInterface _userOrderService;

        public UserOrdersController(UserOrderInterface userOrderService)
        {
            _userOrderService = userOrderService;
        }

        [HttpGet("GetUserOrders")]
        public async Task<IActionResult> FetchAllUserOrders()
        {
            var result = await _userOrderService.GetUserOrders();

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        [HttpGet("GetUserOrder/{id}")]
        public async Task<IActionResult> FetchUserOrderById(int id)
        {
            var result = await _userOrderService.GetUserOrder(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        [HttpPut("PutUserOrders/{id}")]
        public async Task<IActionResult> UpdateUserOrder(int id, [FromBody] UserOrderRequest request)
        {
            var result = await _userOrderService.PutUserOrders(id, request);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        [HttpPost("PostUserOrders")]
        public async Task<IActionResult> CreateUserOrder([FromBody] UserOrderRequest request)
        {
            var result = await _userOrderService.PostUserOrders(request);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }

        [HttpDelete("DeleteUserOrders/{id}")]
        public async Task<IActionResult> RemoveUserOrder(int id)
        {
            var result = await _userOrderService.DeleteUserOrders(id);

            if (!result.Status && result.StatusCode == 404)
                return NotFound(new { status = 404, errors = result.Mensagem });

            return Ok(result);
        }
    }
}
