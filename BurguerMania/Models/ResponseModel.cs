namespace BurguerManiaAPI.Models
{
    // Classe genérica para encapsular a resposta da API.
    public class ResponseModel<T>
    {
        // Dados que serão retornados na resposta.
        public T? Dados { get; set; }

        // Código de status HTTP da resposta.
        public int StatusCode { get; set; }

        // Mensagem informativa sobre a resposta.
        public string Mensagem { get; set; } = string.Empty;

        // Indica se a operação foi bem-sucedida.
        public bool Status { get; set; } = true;
    }
}
