using BurguerManiaAPI.Dto.User;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.User
{
    public interface UserInterface
    {
        // Retorna uma lista de todos os usuários registrados no sistema.
        Task<ResponseModel<List<UsersModel>>> GetUsers();

        // Retorna os detalhes de um usuário específico pelo ID.
        Task<ResponseModel<UsersModel>> GetUser(int id);

        // Cria um novo usuário no sistema usando os dados fornecidos.
        Task<ResponseModel<UsersModel>> PostUsers(UserRequest userRequest);

        // Atualiza as informações de um usuário existente com base no ID e dados fornecidos.
        Task<ResponseModel<UserResponse>> PutUsers(int id, UserRequest userRequest);

        // Remove um usuário do sistema pelo ID especificado.
        Task<ResponseModel<UserResponse>> DeleteUsers(int id);
    }
}
