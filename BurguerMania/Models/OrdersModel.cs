using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    public class OrdersModel
    {
        // Identificador único do pedido.
        [Key]
        public int Id { get; set; }

        // Identificador do status do pedido.
        public int StatusId { get; set; }

        // Valor total do pedido.
        public float Value { get; set; }

        // Objeto de referência ao status do pedido, ignorado na serialização JSON.
        [JsonIgnore]
        public StatusModel? Status { get; set; }
    }
}
