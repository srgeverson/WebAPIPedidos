namespace WebAPIPedidos.API.V1.Model.Request
{
    public class UsuarioLoginRequest
    {
        //private string? _username;
        //private string? _password;
        ///// <summary>
        ///// Nome da conta/usuário para ser autenticado
        ///// </summary>
        //public string? Username { get => _username; set => _username = value; }
        ///// <summary>
        ///// Senha da conta/usuário para ser autenticado
        ///// </summary>
        //public string? Password { get => _password; set => _password = value; }
        public string? client_id { get; set; }
        public string? clientId { get; set; }
        public string? client_secret { get; set; }
        public string? clientSecret { get; set; }
        public string? grant_type { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
