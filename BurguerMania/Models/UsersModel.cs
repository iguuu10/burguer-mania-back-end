using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BurguerManiaAPI.Models
{
    // Representa um usuário do sistema com informações básicas.
    public class UsersModel
    {
        // Identificador único do usuário.
        [Key]
        public int Id { get; set; }

        // Nome completo do usuário.
        public string? Name { get; set; }

        // Endereço de e-mail do usuário.
        public string? Email { get; set; }

        // Senha do usuário (não será serializada por padrão para segurança).
        public string? Password { get; set; }
    }
}
