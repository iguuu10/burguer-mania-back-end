using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    public class OrderProductsModel
    {
        // Identificador único da relação entre um produto e um pedido.
        [Key]
        public int Id { get; set; }

        // Identificador do produto relacionado.
        public int ProductId { get; set; }
        
        // Identificador do pedido relacionado.
        public int OrderId { get; set; }
        
        // Objeto de referência do produto, ignorado na serialização JSON.
        [JsonIgnore]
        public ProductsModel? Product { get; set; }
        
        // Objeto de referência do pedido, ignorado na serialização JSON.
        [JsonIgnore]
        public OrdersModel? Order { get; set; }
    }
}
