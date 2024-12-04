using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.User;
using BurguerManiaAPI.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BurguerManiaAPI.Services.User
{
    public class UserService : UserInterface
    {
        private readonly AppDbContext _context;

        // Construtor que inicializa o contexto de banco de dados
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        public async Task<ResponseModel<List<UsersModel>>> GetUsers()
        {
            ResponseModel<List<UsersModel>> resposta = new ResponseModel<List<UsersModel>>();

            try
            {
                // Verifica se existe algum cliente cadastrado
                if (!_context.Users.Any())
                {
                    resposta.Mensagem = "Nenhum cliente foi encontrado!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }

                var clientes = await _context.Users.ToListAsync();
                resposta.Dados = clientes;
                resposta.Mensagem = "Clientes recuperados com sucesso!";
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
        /// Recupera um cliente específico pelo ID.
        /// </summary>
        public async Task<ResponseModel<UsersModel>> GetUser(int id)
        {
            ResponseModel<UsersModel> resposta = new ResponseModel<UsersModel>();

            try
            {
                var cliente = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = cliente;
                resposta.Mensagem = "Cliente encontrado com sucesso!";
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
        /// Adiciona um novo cliente.
        /// </summary>
        public async Task<ResponseModel<UsersModel>> PostUsers(UserRequest userRequest)
        {
            ResponseModel<UsersModel> resposta = new ResponseModel<UsersModel>();

            try
            {
                var cliente = new UsersModel
                {
                    Name = userRequest.Name,
                    Email = userRequest.Email,
                    Password = userRequest.Password
                };

                await _context.Users.AddAsync(cliente);
                await _context.SaveChangesAsync();

                resposta.Dados = cliente;
                resposta.Mensagem = "Cliente adicionado com sucesso!";
                resposta.StatusCode = 201;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao adicionar cliente: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Atualiza as informações de um cliente existente.
        /// </summary>
        public async Task<ResponseModel<UserResponse>> PutUsers(int id, UserRequest userRequest)
        {
            ResponseModel<UserResponse> resposta = new ResponseModel<UserResponse>();

            try
            {
                var cliente = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                // Atualiza os dados do cliente
                cliente.Name = userRequest.Name;
                cliente.Email = userRequest.Email;
                cliente.Password = userRequest.Password;

                await _context.SaveChangesAsync();

                var clienteResponse = new UserResponse
                {
                    Name = cliente.Name,
                    Email = cliente.Email,
                    Password = cliente.Password
                };

                resposta.Dados = clienteResponse;
                resposta.Mensagem = "Cliente atualizado com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao atualizar cliente: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Deleta um cliente pelo ID.
        /// </summary>
        public async Task<ResponseModel<UserResponse>> DeleteUsers(int id)
        {
            ResponseModel<UserResponse> resposta = new ResponseModel<UserResponse>();

            try
            {
                var cliente = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                _context.Users.Remove(cliente);
                await _context.SaveChangesAsync();

                var clienteResponse = new UserResponse
                {
                    Name = cliente.Name,
                    Email = cliente.Email
                };

                resposta.Dados = clienteResponse;
                resposta.Mensagem = "Cliente removido com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao deletar cliente: {ex.Message}";
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }
    }
}
