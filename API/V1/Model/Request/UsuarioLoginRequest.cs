namespace WebAPIPedidos.API.V1.Model.Request
{
    public class UsuarioLoginRequest
    {
        private string? _login;
        private string? _senha;

        public string? Login { get => _login; set => _login = value; }
        public string? Senha { get => _senha; set => _senha = value; }
    }
}
