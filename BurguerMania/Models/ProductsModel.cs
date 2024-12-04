using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    public class ProductsModel
    {
        // Identificador exclusivo do produto.
        [Key]
        public int Id { get; set; }

        // Nome do produto.
        public string? Name { get; set; }

        // Caminho da imagem do produto.
        public string? PathImage { get; set; }

        // Preço do produto.
        public decimal Price { get; set; }

        // Descrição básica do produto.
        public string? BaseDescription { get; set; }

        // Descrição detalhada do produto.
        public string? FullDescription { get; set; }

        // Identificador da categoria à qual o produto pertence.
        public int? CategoryId { get; set; }

        // Referência à categoria do produto, que será ignorada durante a serialização JSON.
        [JsonIgnore]
        public CategorysModel? Category { get; set; }
    }
}
