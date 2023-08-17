﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAPIPedidos.Core;
using WebAPIPedidos.Domain.Model.DTO;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.Domain.Facade;
public interface IAutenticacaoFacade
{
    UsuarioAutenticadoDTO Autenticar(string login, string senha);
}
public class AutenticacaoFacade : IAutenticacaoFacade
{
    private IUsuarioService _usuarioService;

    public AutenticacaoFacade(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public UsuarioAutenticadoDTO Autenticar(string login, string senha)
    {
        var user = _usuarioService.BuscarPorLogin(login).Result;
        if (user == null || !user.Senha.Equals(senha))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, "1")
            }),
            Expires = DateTime.UtcNow.AddSeconds(WebAPIPedido.TEMPO_EM_SEGUNDOS_TOKEN),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(WebAPIPedido.SECRET_KEY), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var usuarioLogado = new UsuarioAutenticadoDTO();
        usuarioLogado.Token = tokenHandler.WriteToken(token);

        return usuarioLogado;
    }
}
