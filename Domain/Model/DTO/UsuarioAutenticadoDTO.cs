namespace WebAPIPedidos.Domain.Model.DTO;
public class UsuarioAutenticadoDTO
{
    private string? access_token;
    private string? issued_token_type;
    private string? token_type;
    private DateTime? expires_in;

    public string? Access_token { get => access_token; set => access_token = value; }
    public string? Issued_token_type { get => issued_token_type; set => issued_token_type = value; }
    public string? Token_type { get => token_type; set => token_type = value; }
    public DateTime? Expires_in { get => expires_in; set => expires_in = value; }
}
