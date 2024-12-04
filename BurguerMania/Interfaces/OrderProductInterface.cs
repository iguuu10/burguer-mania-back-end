using BurguerManiaAPI.Dto.OrderProduct; 
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.OrderProduct
{
    public interface IOrderProductInterface
    {
        // Obtém uma lista de todos os produtos de pedido registrados.
        Task<ResponseModel<List<OrderProductResponse>>> GetOrderProducts();

        // Retorna os detalhes de um produto de pedido específico, identificado pelo ID.
        Task<ResponseModel<OrderProductResponse>> GetOrderProduct(int id);

        // Adiciona um novo produto ao pedido.
        Task<ResponseModel<OrderProductResponse>> PostOrderProducts(OrderProductRequest orderProductRequest);

        // Atualiza as informações de um produto de pedido existente com base no ID.
        Task<ResponseModel<OrderProductResponse>> PutOrderProducts(int id, OrderProductRequest orderProductRequest);

        // Remove um produto de pedido do sistema pelo ID especificado.
        Task<ResponseModel<OrderProductResponse>> DeleteOrderProducts(int id);
    }
}
