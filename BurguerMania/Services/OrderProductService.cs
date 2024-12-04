using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.OrderProduct;
using BurguerManiaAPI.Interfaces.OrderProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurguerManiaAPI.Services.OrderProduct
{
    public class OrderProductService : IOrderProductInterface
    {
        private readonly AppDbContext _context;

        public OrderProductService(AppDbContext context)
        {
            _context = context;
        }

        // Método para obter todos os produtos de pedidos.
        public async Task<ResponseModel<List<OrderProductResponse>>> GetOrderProducts()
        {
            var response = new ResponseModel<List<OrderProductResponse>>();
            try
            {
                var orderProducts = await _context.OrdersProducts
                    .Select(op => new OrderProductResponse
                    {
                        OrderId = op.OrderId,
                        ProductId = op.ProductId
                    })
                    .ToListAsync();

                if (orderProducts.Count == 0)
                {
                    response.Mensagem = "Nenhum produto de pedido encontrado no sistema!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                response.Dados = orderProducts;
                response.Mensagem = "Produtos de pedidos recuperados com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao recuperar produtos de pedidos: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para obter um produto de pedido específico pelo ID.
        public async Task<ResponseModel<OrderProductResponse>> GetOrderProduct(int id)
        {
            var response = new ResponseModel<OrderProductResponse>();
            try
            {
                var orderProduct = await _context.OrdersProducts
                    .Where(op => op.OrderId == id)
                    .Select(op => new OrderProductResponse
                    {
                        OrderId = op.OrderId,
                        ProductId = op.ProductId
                    })
                    .FirstOrDefaultAsync();

                if (orderProduct == null)
                {
                    response.Mensagem = "Produto de pedido com o ID especificado não encontrado!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                response.Dados = orderProduct;
                response.Mensagem = "Produto de pedido encontrado com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar produto de pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para adicionar um novo produto de pedido.
        public async Task<ResponseModel<OrderProductResponse>> PostOrderProducts(OrderProductRequest orderProductRequest)
        {
            var response = new ResponseModel<OrderProductResponse>();
            try
            {
                var orderProduct = new OrderProductsModel
                {
                    OrderId = orderProductRequest.OrderId,
                    ProductId = orderProductRequest.ProductId
                };

                _context.OrdersProducts.Add(orderProduct);
                await _context.SaveChangesAsync();

                response.Dados = new OrderProductResponse
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId
                };

                response.Mensagem = "Produto de pedido cadastrado com êxito!";
                response.StatusCode = 201; 
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao cadastrar produto de pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para atualizar um produto de pedido existente.
        public async Task<ResponseModel<OrderProductResponse>> PutOrderProducts(int id, OrderProductRequest orderProductRequest)
        {
            var response = new ResponseModel<OrderProductResponse>();
            try
            {
                var orderProduct = await _context.OrdersProducts
                    .FirstOrDefaultAsync(op => op.OrderId == id);

                if (orderProduct == null)
                {
                    response.Mensagem = "Produto de pedido com o ID especificado não encontrado!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                orderProduct.ProductId = orderProductRequest.ProductId;
                orderProduct.OrderId = orderProductRequest.OrderId;

                await _context.SaveChangesAsync();

                response.Dados = new OrderProductResponse
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId
                };

                response.Mensagem = "Produto de pedido atualizado com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao atualizar produto de pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para remover um produto de pedido pelo ID.
        public async Task<ResponseModel<OrderProductResponse>> DeleteOrderProducts(int id)
        {
            var response = new ResponseModel<OrderProductResponse>();
            try
            {
                var orderProduct = await _context.OrdersProducts
                    .FirstOrDefaultAsync(op => op.OrderId == id);

                if (orderProduct == null)
                {
                    response.Mensagem = "Produto de pedido com o ID especificado não encontrado!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                _context.OrdersProducts.Remove(orderProduct);
                await _context.SaveChangesAsync();

                response.Dados = new OrderProductResponse
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId
                };

                response.Mensagem = "Produto de pedido excluído com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao excluir produto de pedido: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
