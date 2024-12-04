using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.Product
{
    public class ProductRequest
    {
        // O nome do produto é obrigatório e deve ter entre 6 a 100 caracteres.
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O nome deve ter entre 6 e 100 caracteres.")]
        public string? Name { get; set; }

        // A descrição do produto é obrigatória e deve ter entre 6 a 250 caracteres.
        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "A descrição deve ter no máximo 250 caracteres e no mínimo 6.")]
        public string? Description { get; set; }

        // A imagem do produto é obrigatória.
        [Required(ErrorMessage = "A imagem do produto é obrigatória.")]
        public string? PathImage { get; set; }

        // O preço do produto é obrigatório e deve estar entre 0,01 e 999,99.
        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, 999.99, ErrorMessage = "O preço deve estar entre 0,01 e 999,99.")]
        public decimal Price { get; set; }

        // A categoria do produto é obrigatória.
        [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
        public int CategoryId { get; set; }

        // A descrição base pode ter no máximo 150 caracteres e no mínimo 6.
        [StringLength(150, MinimumLength = 6, ErrorMessage = "A descrição base deve ter no máximo 150 caracteres e no mínimo 6.")]
        public string? BaseDescription { get; set; }

        // A descrição completa pode ter no máximo 1000 caracteres e no mínimo 6.
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "A descrição completa deve ter no máximo 1000 caracteres e no mínimo 6.")]
        public string? FullDescription { get; set; }
    }
}
