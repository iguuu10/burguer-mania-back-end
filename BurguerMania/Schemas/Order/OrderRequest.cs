using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.Order
{
    public class OrderRequest
    {
        // O StatusId é um campo obrigatório.
        [Required(ErrorMessage = "É necessário informar o StatusId.")]
        public int StatusId { get; set; }

        // O valor do pedido é obrigatório e deve ser um número positivo.
        [Required(ErrorMessage = "O valor do pedido é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser um número positivo.")]
        public float Value { get; set; }
    }
}
