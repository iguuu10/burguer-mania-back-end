using BurguerManiaAPI.Dto.UserOrder;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.UserOrder
{
    public interface UserOrderInterface
    {
        // Retorna uma lista de todos os pedidos de usuários no sistema.
        Task<ResponseModel<List<UserOrdersModel>>> GetUserOrders();

        // Retorna os detalhes de um pedido específico de um usuário pelo ID.
        Task<ResponseModel<UserOrdersModel>> GetUserOrder(int id);

        // Cria um novo pedido de usuário usando os dados fornecidos.
        Task<ResponseModel<UserOrdersModel>> PostUserOrders(UserOrderRequest userOrderRequest);

        // Atualiza as informações de um pedido de usuário existente com base no ID e dados fornecidos.
        Task<ResponseModel<UserOrderResponse>> PutUserOrders(int id, UserOrderRequest userOrderRequest);

        // Remove um pedido de usuário do sistema pelo ID especificado.
        Task<ResponseModel<UserOrderResponse>> DeleteUserOrders(int id);
    }
}
