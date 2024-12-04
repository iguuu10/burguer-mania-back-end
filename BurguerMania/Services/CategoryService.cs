using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;
using BurguerManiaAPI.Context;
using BurguerManiaAPI.Dto.Category;
using BurguerManiaAPI.Interfaces.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurguerManiaAPI.Services.Category
{
    public class CategoryService : CategoryInterface
    {
        private readonly AppDbContext _dbContext;

        // Construtor que injeta o contexto do banco de dados.
        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para obter todas as categorias.
        public async Task<ResponseModel<List<CategoryResponse>>> GetCategories()
        {
            var response = new ResponseModel<List<CategoryResponse>>();
            try
            {
                var categoriesList = await _dbContext.Categories
                    .Select(c => new CategoryResponse
                    {
                        Name = c.Name,
                        Description = c.Description,
                        PathImage = c.PathImage
                    })
                    .ToListAsync();

                if (!categoriesList.Any())
                {
                    response.Mensagem = "Nenhuma categoria foi encontrada!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                response.Dados = categoriesList;
                response.Mensagem = "Categorias recuperadas com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para obter uma categoria específica pelo ID.
        public async Task<ResponseModel<CategoryResponse>> GetCategory(int id)
        {
            var response = new ResponseModel<CategoryResponse>();
            try
            {
                var category = await _dbContext.Categories
                    .Where(c => c.Id == id)
                    .Select(c => new CategoryResponse
                    {
                        Name = c.Name,
                        Description = c.Description,
                        PathImage = c.PathImage
                    })
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                response.Dados = category;
                response.Mensagem = "Categoria encontrada!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para criar uma nova categoria.
        public async Task<ResponseModel<CategoryResponse>> PostCategories(CategoryRequest categoryRequest)
        {
            var response = new ResponseModel<CategoryResponse>();
            try
            {
                var newCategory = new CategorysModel
                {
                    Name = categoryRequest.Name,
                    Description = categoryRequest.Description,
                    PathImage = categoryRequest.PathImage
                };

                await _dbContext.Categories.AddAsync(newCategory);
                await _dbContext.SaveChangesAsync();

                response.Dados = new CategoryResponse
                {
                    Name = newCategory.Name,
                    Description = newCategory.Description,
                    PathImage = newCategory.PathImage
                };

                response.Mensagem = "Categoria criada com sucesso!";
                response.StatusCode = 201;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para atualizar uma categoria existente.
        public async Task<ResponseModel<CategoryResponse>> PutCategories(int id, CategoryRequest categoryRequest)
        {
            var response = new ResponseModel<CategoryResponse>();
            try
            {
                var categoryToUpdate = await _dbContext.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (categoryToUpdate == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                categoryToUpdate.Name = categoryRequest.Name;
                categoryToUpdate.Description = categoryRequest.Description;
                categoryToUpdate.PathImage = categoryRequest.PathImage;

                await _dbContext.SaveChangesAsync();

                response.Dados = new CategoryResponse
                {
                    Name = categoryToUpdate.Name,
                    Description = categoryToUpdate.Description,
                    PathImage = categoryToUpdate.PathImage
                };

                response.Mensagem = "Categoria atualizada com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }

        // Método para excluir uma categoria existente.
        public async Task<ResponseModel<CategoryResponse>> DeleteCategories(int id)
        {
            var response = new ResponseModel<CategoryResponse>();
            try
            {
                var categoryToDelete = await _dbContext.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (categoryToDelete == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    response.Status = false;
                    response.StatusCode = 404;
                    return response;
                }

                _dbContext.Categories.Remove(categoryToDelete);
                await _dbContext.SaveChangesAsync();

                response.Dados = new CategoryResponse
                {
                    Name = categoryToDelete.Name,
                    Description = categoryToDelete.Description,
                    PathImage = categoryToDelete.PathImage
                };

                response.Mensagem = "Categoria excluída com sucesso!";
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro: {ex.Message}";
                response.Status = false;
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
