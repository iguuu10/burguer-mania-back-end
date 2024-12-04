using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BurguerManiaAPI.Models
{
    public class CategorysModel
    {
        // Identificador único da categoria.
        [Key]
        public int Id { get; set; }

        // Nome da categoria.
        public string? Name { get; set; }

        // Descrição detalhada da categoria.
        public string? Description { get; set; }

        // Caminho da imagem representativa da categoria.
        public string? PathImage { get; set; }

        // Lista de produtos associados a essa categoria.
        public ICollection<ProductsModel> Products { get; set; } = new List<ProductsModel>();
    }
}
