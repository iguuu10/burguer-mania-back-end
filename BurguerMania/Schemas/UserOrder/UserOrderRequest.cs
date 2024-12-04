using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.UserOrder
{
    public class UserOrderRequest
    {
        // O ID do cliente é obrigatório para criar um pedido.
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int UserId { get; set; }

        // O ID do pedido é obrigatório para associar o cliente ao pedido.
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public int OrderId { get; set; }
    }
}
