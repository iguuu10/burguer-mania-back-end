using BurguerManiaAPI.Dto.Order;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.Order
{
    public interface OrderInterface
    {
        // Retorna uma lista de todas as ordens registradas.
        Task<ResponseModel<List<OrderResponse>>> GetOrders();

        // Retorna os detalhes de uma ordem específica com base no ID.
        Task<ResponseModel<OrderResponse>> GetOrder(int id);

        // Cria uma nova ordem no sistema.
        Task<ResponseModel<OrderResponse>> PostOrders(OrderRequest orderRequest);

        // Atualiza as informações de uma ordem existente usando seu ID.
        Task<ResponseModel<OrderResponse>> PutOrders(int id, OrderRequest orderRequest);

        // Exclui uma ordem do sistema pelo ID fornecido.
        Task<ResponseModel<OrderResponse>> DeleteOrders(int id);
    }
}
