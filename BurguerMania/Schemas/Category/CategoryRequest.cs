using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.Category
{
    public class CategoryRequest
    {
        // O nome da categoria é obrigatório.
        [Required(ErrorMessage = "Por favor, informe o nome da categoria.")]
        public string? Name { get; set; }

        // A descrição é obrigatória e deve ter entre 6 e 150 caracteres.
        [Required(ErrorMessage = "A descrição da categoria é necessária.")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "A descrição deve ter no máximo 150 caracteres e no mínimo 6 caracteres.")]
        public string? Description { get; set; }
        
        // O caminho da imagem é um campo obrigatório.
        [Required(ErrorMessage = "O caminho da imagem deve ser informado.")]
        public string? PathImage { get; set; }
    }
}
