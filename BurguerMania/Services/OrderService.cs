using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.Order;
using BurguerManiaAPI.Interfaces.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurguerManiaAPI.Services.Order
{
    public class OrderService : OrderInterface
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // Método para listar todos os pedidos.
        public async Task<ResponseModel<List<OrderResponse>>> GetOrders()
        {
            var response = new ResponseModel<List<OrderResponse>>();
            try
            {
                var orders = await _context.Orders
                    .Select(o => new OrderResponse
                    {
                        StatusId = o.StatusId,
                        Status = o.Status != null ? o.Status.Name : "Status indisponível",
                        Value = o.Value
                    })
                    .ToListAsync();

                if (!orders.Any())
                {
                    response.Mensagem = "Nenhum pedido foi encontrado na base de dados.";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                response.Dados = orders;
                response.Mensagem = "Pedidos obtidos com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Ocorreu um erro inesperado: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para obter detalhes de um pedido específico pelo ID.
        public async Task<ResponseModel<OrderResponse>> GetOrder(int id)
        {
            var response = new ResponseModel<OrderResponse>();
            try
            {
                var order = await _context.Orders
                    .Where(o => o.Id == id)
                    .Select(o => new OrderResponse
                    {
                        StatusId = o.StatusId,
                        Status = o.Status != null ? o.Status.Name : "Status indisponível",
                        Value = o.Value
                    })
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    response.Mensagem = "Pedido não encontrado na base de dados.";
                    response.StatusCode = 404;
                    response.Status = false;
                    return response;
                }

                response.Dados = order;
                response.Mensagem = "Pedido recuperado com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para criar um novo pedido.
        public async Task<ResponseModel<OrderResponse>> PostOrders(OrderRequest orderRequest)
        {
            var response = new ResponseModel<OrderResponse>();
            try
            {
                var newOrder = new OrdersModel
                {
                    StatusId = orderRequest.StatusId,
                    Value = orderRequest.Value
                };

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                var orderResponse = new OrderResponse
                {
                    Status = newOrder.Status != null ? newOrder.Status.Name : "Status indisponível",
                    Value = newOrder.Value
                };

                response.Dados = orderResponse;
                response.Mensagem = "Novo pedido cadastrado com sucesso!";
                response.StatusCode = 201;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar o pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para atualizar um pedido existente.
        public async Task<ResponseModel<OrderResponse>> PutOrders(int id, OrderRequest orderRequest)
        {
            var response = new ResponseModel<OrderResponse>();
            try
            {
                var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (orderToUpdate == null)
                {
                    response.Mensagem = "Pedido não encontrado para atualização.";
                    response.StatusCode = 404;
                    response.Status = false;
                    return response;
                }

                orderToUpdate.StatusId = orderRequest.StatusId;
                orderToUpdate.Value = orderRequest.Value;

                await _context.SaveChangesAsync();

                var updatedOrderResponse = new OrderResponse
                {
                    Status = orderToUpdate.Status != null ? orderToUpdate.Status.Name : "Status indisponível",
                    Value = orderToUpdate.Value
                };

                response.Dados = updatedOrderResponse;
                response.Mensagem = "Pedido atualizado com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao atualizar o pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para remover um pedido pelo ID.
        public async Task<ResponseModel<OrderResponse>> DeleteOrders(int id)
        {
            var response = new ResponseModel<OrderResponse>();
            try
            {
                var orderToDelete = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

                if (orderToDelete == null)
                {
                    response.Mensagem = "Pedido não encontrado para exclusão.";
                    response.StatusCode = 404;
                    response.Status = false;
                    return response;
                }

                _context.Orders.Remove(orderToDelete);
                await _context.SaveChangesAsync();

                var deletedOrderResponse = new OrderResponse
                {
                    Status = orderToDelete.Status != null ? orderToDelete.Status.Name : "Status indisponível",
                    Value = orderToDelete.Value
                };

                response.Dados = deletedOrderResponse;
                response.Mensagem = "Pedido excluído com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao deletar o pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
