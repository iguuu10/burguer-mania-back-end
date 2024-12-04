using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.Product;
using BurguerManiaAPI.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BurguerManiaAPI.Services.Product
{
    /// <summary>
    /// Serviço responsável pela gestão de produtos.
    /// </summary>
    public class ProductService : ProductInterface
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de todos os produtos cadastrados.
        /// </summary>
        /// <returns>Um modelo de resposta com a lista de produtos</returns>
        public async Task<ResponseModel<List<ProductResponse>>> GetProducts()
        {
            ResponseModel<List<ProductResponse>> resposta = new ResponseModel<List<ProductResponse>>();

            try
            {
                // Verifica se existem produtos cadastrados
                if (!_context.Products.Any())
                {
                    resposta.Mensagem = "Não há produtos cadastrados!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }

                // Consulta produtos com suas categorias associadas
                var produtos = await _context.Products
                    .Include(x => x.Category)
                    .Select(x => new ProductResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        CategoryName = x.Category != null ? x.Category.Name : "Sem categoria",
                        PathImage = x.PathImage,
                        BaseDescription = x.BaseDescription,
                        FullDescription = x.FullDescription
                    }).ToListAsync();

                resposta.Dados = produtos;
                resposta.Mensagem = "Todos os produtos foram coletados!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura e trata qualquer exceção que ocorra
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Obtém os detalhes de um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser buscado</param>
        /// <returns>Um modelo de resposta com as informações do produto</returns>
        public async Task<ResponseModel<ProductResponse>> GetProduct(int id)
        {
            ResponseModel<ProductResponse> resposta = new ResponseModel<ProductResponse>();

            try
            {
                // Busca o produto pelo ID com a categoria associada
                var produto = await _context.Products
                    .Include(x => x.Category)
                    .Where(x => x.Id == id)
                    .Select(x => new ProductResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        CategoryName = x.Category != null ? x.Category.Name : "Sem categoria",
                        PathImage = x.PathImage,
                        BaseDescription = x.BaseDescription,
                        FullDescription = x.FullDescription
                    }).FirstOrDefaultAsync();

                if (produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = produto;
                resposta.Mensagem = "Produto coletado!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura e trata qualquer exceção que ocorra
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados.
        /// </summary>
        /// <param name="productRequest">Objeto contendo as informações do produto</param>
        /// <returns>Um modelo de resposta com os detalhes do produto cadastrado</returns>
        public async Task<ResponseModel<ProductResponse>> PostProducts(ProductRequest productRequest)
        {
            ResponseModel<ProductResponse> resposta = new ResponseModel<ProductResponse>();

            try
            {
                // Cria um novo produto a partir do request
                ProductsModel produto = new ProductsModel
                {
                    Name = productRequest.Name,
                    Price = productRequest.Price,
                    CategoryId = productRequest.CategoryId,
                    PathImage = productRequest.PathImage,
                    BaseDescription = productRequest.BaseDescription,
                    FullDescription = productRequest.FullDescription
                };

                _context.Products.Add(produto);
                await _context.SaveChangesAsync();

                // Verifica se a categoria existe antes de criar a resposta
                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id == productRequest.CategoryId);

                if (category == null)
                {
                    resposta.Mensagem = "Categoria inválida!";
                    resposta.Status = false;
                    resposta.StatusCode = 400;
                    return resposta;
                }

                // Cria a resposta do produto cadastrado
                var produtoResponse = new ProductResponse
                {
                    Id = produto.Id,
                    Name = produto.Name,
                    Price = produto.Price,
                    CategoryName = produto.Category != null ? produto.Category.Name : "Sem categoria",
                    PathImage = produto.PathImage,
                    BaseDescription = produto.BaseDescription,
                    FullDescription = produto.FullDescription
                };

                resposta.Dados = produtoResponse;
                resposta.Mensagem = "Produto cadastrado!";
                resposta.StatusCode = 201;
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura e trata qualquer exceção que ocorra
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Atualiza um produto existente pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado</param>
        /// <param name="productRequest">Objeto contendo as informações atualizadas do produto</param>
        /// <returns>Um modelo de resposta com os detalhes do produto atualizado</returns>
        public async Task<ResponseModel<ProductResponse>> PutProducts(int id, ProductRequest productRequest)
        {
            ResponseModel<ProductResponse> resposta = new ResponseModel<ProductResponse>();

            try
            {
                // Busca o produto atual pelo ID
                var produtoAtual = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (produtoAtual == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                // Verifica se a categoria existe
                var categoria = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productRequest.CategoryId);
                if (categoria == null)
                {
                    resposta.Mensagem = "Categoria inválida!";
                    resposta.Status = false;
                    resposta.StatusCode = 400;
                    return resposta;
                }

                // Atualiza as propriedades do produto
                produtoAtual.Name = productRequest.Name;
                produtoAtual.Price = productRequest.Price;
                produtoAtual.BaseDescription = productRequest.BaseDescription;
                produtoAtual.FullDescription = productRequest.FullDescription;
                produtoAtual.CategoryId = productRequest.CategoryId;
                produtoAtual.PathImage = productRequest.PathImage;

                await _context.SaveChangesAsync();

                // Cria a resposta com os dados atualizados
                var produtoResponse = new ProductResponse
                {
                    Id = produtoAtual.Id,
                    Name = produtoAtual.Name,
                    Price = produtoAtual.Price,
                    CategoryName = produtoAtual.Category != null ? produtoAtual.Category.Name : "Sem categoria",
                    PathImage = produtoAtual.PathImage,
                    BaseDescription = produtoAtual.BaseDescription,
                    FullDescription = produtoAtual.FullDescription
                };

                resposta.Dados = produtoResponse;
                resposta.Mensagem = "Produto atualizado!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        /// <summary>
        /// Deleta um produto existente pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser deletado</param>
        /// <returns>Um modelo de resposta com os detalhes do produto deletado</returns>
        public async Task<ResponseModel<ProductResponse>> DeleteProducts(int id)
        {
            ResponseModel<ProductResponse> resposta = new ResponseModel<ProductResponse>();

            try
            {
                // Busca o produto pelo ID, incluindo a categoria associada
                var produto = await _context.Products
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }

                // Remove o produto do banco de dados
                _context.Products.Remove(produto);
                await _context.SaveChangesAsync();

                // Cria a resposta com os detalhes do produto deletado
                var produtoResponse = new ProductResponse
                {
                    Id = produto.Id,
                    Name = produto.Name,
                    Price = produto.Price,
                    CategoryName = produto.Category != null ? produto.Category.Name : "Sem categoria",
                    PathImage = produto.PathImage,
                    BaseDescription = produto.BaseDescription,
                    FullDescription = produto.FullDescription
                };

                resposta.Dados = produtoResponse;
                resposta.Mensagem = "Produto deletado!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura e trata qualquer exceção que ocorra
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }
    }
}
