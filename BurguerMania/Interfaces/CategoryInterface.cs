using BurguerManiaAPI.Dto.Category;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.Category
{
    public interface CategoryInterface
    {
        // Recupera uma lista de todas as categorias disponíveis.
        Task<ResponseModel<List<CategoryResponse>>> GetCategories();

        // Obtém os detalhes de uma categoria específica com base no ID.
        Task<ResponseModel<CategoryResponse>> GetCategory(int id);

        // Adiciona uma nova categoria ao sistema.
        Task<ResponseModel<CategoryResponse>> PostCategories(CategoryRequest categoryRequest);

        // Atualiza as informações de uma categoria existente usando seu ID.
        Task<ResponseModel<CategoryResponse>> PutCategories(int id, CategoryRequest categoryRequest);

        // Remove uma categoria do sistema usando o ID.
        Task<ResponseModel<CategoryResponse>> DeleteCategories(int id);
    }
}
