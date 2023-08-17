using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WebAPIPedidos.Domain.Model.DTO;
using WebAPIPedidos.Domain.Model.Entity;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.Domain.Facade;
public interface IAutenticacaoFacade
{
    UsuarioAutenticadoDTO Autenticar(string login, string senha);
    string RefreshToken();
    string GenerateToken(string nome, string role);
    byte[] GenerateKey(int bytes);
    JsonWebKey CreateJWK();
    SecurityKey Loadkey();
}
public class AutenticacaoFacade : IAutenticacaoFacade
{
    private IUsuarioService _usuarioService;
    private RandomNumberGenerator _randomNumberGenerator;
    private static readonly string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "random.json");

    public AutenticacaoFacade(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        _randomNumberGenerator = RandomNumberGenerator.Create();
    }

    public UsuarioAutenticadoDTO Autenticar(string login, string senha)
    {
        var user = _usuarioService.BuscarPorLogin(login).Result;
        if (user == null || !user.Senha.Equals(senha))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("261d901b2208e2306405d03a95541b1cb5047266");//Colocar o segredo aqui
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, "1")
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var usuarioLogado = new UsuarioAutenticadoDTO();
        usuarioLogado.Token = tokenHandler.WriteToken(token);

        return usuarioLogado;
    }

    public string RefreshToken()
    {
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
    public string GenerateToken(string nome, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Loadkey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Role, role),
            }),
            Expires = DateTime.UtcNow.AddSeconds(15000),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public byte[] GenerateKey(int bytes)
    {
        var data = new byte[bytes];
        _randomNumberGenerator.GetBytes(data);
        return data;
    }

    public JsonWebKey CreateJWK()
    {
        var symetricKey = new HMACSHA256(GenerateKey(64));
        var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
        jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
        return jwk;
    }
    public SecurityKey Loadkey()
    {
        if (File.Exists(MyJwkLocation))
            return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

        var newKey = CreateJWK();
        File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
        return newKey;
    }
}
