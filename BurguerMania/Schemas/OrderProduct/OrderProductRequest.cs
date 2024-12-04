using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.OrderProduct
{
    public class OrderProductRequest
    {
        // O ID do pedido é obrigatório.
        [Required(ErrorMessage = "O campo PedidoId deve ser preenchido.")]
        public int OrderId { get; set; }

        // O ID do produto é obrigatório.
        [Required(ErrorMessage = "O campo ProdutoId deve ser preenchido.")]
        public int ProductId { get; set; }
    }
}
