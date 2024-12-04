using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.UserOrder;
using BurguerManiaAPI.Interfaces.UserOrder;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BurguerManiaAPI.Services.UserOrder
{
    public class UserOrderService : UserOrderInterface
    {
        private readonly AppDbContext _context;

        // Construtor para injeção de dependência do contexto de banco de dados
        public UserOrderService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os pedidos cadastrados.
        /// </summary>
        public async Task<ResponseModel<List<UserOrdersModel>>> GetUserOrders()
        {
            ResponseModel<List<UserOrdersModel>> resposta = new ResponseModel<List<UserOrdersModel>>();

            try
            {
                // Verifica se existem pedidos no banco de dados
                if (!_context.UsersOrders.Any())
                {
                    resposta.Mensagem = "Nenhum pedido foi encontrado!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }

                var pedidos = await _context.UsersOrders.ToListAsync();
                resposta.Dados = pedidos;
                resposta.Mensagem = "Pedidos coletados com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura e trata possíveis exceções
                resposta.Mensagem = $"Erro inesperado: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Obtém um pedido específico pelo seu ID.
        /// </summary>
        public async Task<ResponseModel<UserOrdersModel>> GetUserOrder(int id)
        {
            ResponseModel<UserOrdersModel> resposta = new ResponseModel<UserOrdersModel>();

            try
            {
                var pedido = await _context.UsersOrders.FirstOrDefaultAsync(x => x.Id == id);

                if (pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = pedido;
                resposta.Mensagem = "Pedido recuperado com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro inesperado: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Cadastra um novo pedido.
        /// </summary>
        public async Task<ResponseModel<UserOrdersModel>> PostUserOrders(UserOrderRequest userOrderRequest)
        {
            ResponseModel<UserOrdersModel> resposta = new ResponseModel<UserOrdersModel>();

            try
            {
                var pedidoModel = new UserOrdersModel
                {
                    UserId = userOrderRequest.UserId,
                    OrderId = userOrderRequest.OrderId
                };

                _context.UsersOrders.Add(pedidoModel);
                await _context.SaveChangesAsync();

                resposta.Dados = pedidoModel;
                resposta.Mensagem = "Pedido cadastrado com sucesso!";
                resposta.StatusCode = 201;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao cadastrar o pedido: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Atualiza um pedido existente pelo ID.
        /// </summary>
        public async Task<ResponseModel<UserOrderResponse>> PutUserOrders(int id, UserOrderRequest userOrderRequest)
        {
            ResponseModel<UserOrderResponse> resposta = new ResponseModel<UserOrderResponse>();

            try
            {
                var pedidoModel = await _context.UsersOrders.FirstOrDefaultAsync(x => x.Id == id);

                if (pedidoModel == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                pedidoModel.UserId = userOrderRequest.UserId;
                pedidoModel.OrderId = userOrderRequest.OrderId;
                await _context.SaveChangesAsync();

                var pedidoResponse = new UserOrderResponse
                {
                    UserId = pedidoModel.UserId,
                    OrderId = pedidoModel.OrderId
                };

                resposta.Dados = pedidoResponse;
                resposta.Mensagem = "Pedido atualizado com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao atualizar o pedido: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Deleta um pedido pelo ID.
        /// </summary>
        public async Task<ResponseModel<UserOrderResponse>> DeleteUserOrders(int id)
        {
            ResponseModel<UserOrderResponse> resposta = new ResponseModel<UserOrderResponse>();

            try
            {
                var pedido = await _context.UsersOrders.FirstOrDefaultAsync(x => x.Id == id);

                if (pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                var pedidoResponse = new UserOrderResponse
                {
                    UserId = pedido.UserId,
                    OrderId = pedido.OrderId
                };

                _context.UsersOrders.Remove(pedido);
                await _context.SaveChangesAsync();

                resposta.Dados = pedidoResponse;
                resposta.Mensagem = "Pedido removido com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao deletar o pedido: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }
    }
}
