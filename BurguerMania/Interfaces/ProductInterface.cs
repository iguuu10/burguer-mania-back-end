using BurguerManiaAPI.Dto.Product;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Interfaces.Product
{
    public interface ProductInterface
    {
        // Recupera todos os produtos registrados no sistema.
        Task<ResponseModel<List<ProductResponse>>> GetProducts();

        // Obtém os detalhes de um produto específico pelo seu ID.
        Task<ResponseModel<ProductResponse>> GetProduct(int id);

        // Adiciona um novo produto ao sistema com base nos dados fornecidos.
        Task<ResponseModel<ProductResponse>> PostProducts(ProductRequest productRequest);

        // Atualiza as informações de um produto existente usando seu ID e os dados de atualização.
        Task<ResponseModel<ProductResponse>> PutProducts(int id, ProductRequest productRequest);

        // Exclui um produto do sistema pelo ID especificado.
        Task<ResponseModel<ProductResponse>> DeleteProducts(int id);
    }
}
