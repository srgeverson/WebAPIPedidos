namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de um usuário autenticado
    /// </summary>
    public class UsuarioAutenticadoResponse
    {
        private string? _token;
        private string? _nome;

        public string? Token { get => _token; set => _token = value; }
        public string? Nome { get => _nome; set => _nome = value; }
    }
}
