using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.ExceptionHandler;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Facade;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsuarioController : ControllerBase
{
    private IAutenticacaoFacade _autenticacaoFacade;
    private IUsuarioMapper _usuarioMapper;
    public UsuarioController(IAutenticacaoFacade autenticacaoFacade, IUsuarioMapper usuarioMapper)
    {
        _autenticacaoFacade = autenticacaoFacade;
        _usuarioMapper = usuarioMapper;
    }

    #region Documentação
    /// <summary>
    /// Autentica o usuário.
    /// </summary>
    /// <response code="200">Usuário autenticado.</response>
    /// <response code="400">Crítica durante a autenticação.</response>
    /// <response code="401">Acesso não permitido.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(UsuarioAutenticadoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpPost("login"), MapToApiVersion("1.0")]
    [AllowAnonymous]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult Login(
        //[FromBody]
        [FromForm]

    //UsuarioLoginRequest usuarioLoginRequest
        //string data
        IFormCollection usuarioLoginRequest//, [FromBody] string teste
                                           //, string username, string password
                                           //, [FromHeader] IHeaderDictionary header
        )
    {
        try
        {
            //var usuarioAutenticado = _autenticacaoFacade.Autenticar(usuarioLoginRequest.Username, usuarioLoginRequest.Password);
            //if (usuarioAutenticado == null)
            //    throw new ProblemaException(StatusCodes.Status401Unauthorized, "Acesso não autorizado");
            //var usuarioAutenticadoResponse = _usuarioMapper.ToAutenticadoResponse(usuarioAutenticado);
            //return Ok(usuarioAutenticadoResponse);
            return Ok(usuarioLoginRequest);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Autenticacao", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemaResponse() { Codigo = StatusCodes.Status500InternalServerError, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }
}
