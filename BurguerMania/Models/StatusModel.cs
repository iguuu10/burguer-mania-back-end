using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    // Representa o status de um pedido ou outro elemento no sistema.
    public class StatusModel
    {
        // Identificador único do status.
        [Key]
        public int Id { get; set; }

        // Nome do status (ex: "Em preparação", "Finalizado").
        public string? Name { get; set; }
    }
}
