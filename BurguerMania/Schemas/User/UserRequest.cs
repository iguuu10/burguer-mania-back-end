using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BurguerManiaAPI.Dto.User
{
    public class UserRequest
    {
        // O nome do usuário é obrigatório e deve ter entre 5 a 100 caracteres.
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres.")]
        public string? Name { get; set; }

        // O email do usuário é obrigatório e deve ser um endereço de email válido, com no máximo 50 caracteres.
        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O email pode ter no máximo 50 caracteres.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de email válido.")]
        public string? Email { get; set; }

        // A senha do usuário é obrigatória e deve ter entre 6 e 8 caracteres.
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 8 caracteres.")]
        [JsonIgnore]
        public string? Password { get; set; }
    }
}
