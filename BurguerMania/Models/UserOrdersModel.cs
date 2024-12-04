using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    // Representa a associação entre um usuário e um pedido.
    public class UserOrdersModel
    {
        // Identificador único para a relação entre usuário e pedido.
        [Key]
        public int Id { get; set; }

        // Identificador do usuário associado a este pedido.
        public int UserId { get; set; }

        // Identificador do pedido associado a este usuário.
        public int OrderId { get; set; }

        // Referência ao pedido relacionado, ignorada na serialização JSON.
        [JsonIgnore]
        public OrdersModel? Order { get; set; }

        // Referência ao usuário relacionado, ignorada na serialização JSON.
        [JsonIgnore]
        public UsersModel? User { get; set; }
    }
}
