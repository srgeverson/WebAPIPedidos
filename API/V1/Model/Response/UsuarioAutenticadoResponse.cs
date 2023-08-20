namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de um usuário autenticado
    /// https://datatracker.ietf.org/doc/html/rfc8693#name-response
    /// </summary>
    public class UsuarioAutenticadoResponse
    {
        /// <summary>
        /// Nome do usuário autenticado
        /// </summary>
        public string? access_token { get; set; }
        public string? issued_token_type { get; set; }
        public string? token_type { get; set; }
        public DateTime? expires_in { get; set; }
    }
}
