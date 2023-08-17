namespace WebAPIPedidos.Domain.Model.DTO;
public class UsuarioAutenticadoDTO
{
    private string? _token;
    private string? _nome;

    public string? Token { get => _token; set => _token = value; }
    public string? Nome { get => _nome; set => _nome = value; }
}
