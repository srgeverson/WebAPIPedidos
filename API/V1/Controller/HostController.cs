using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.ExceptionHandler;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Core;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize("ApiScope")]
public class HostController : ControllerBase
{
    public HostController() { }

    #region Documentação
    /// <summary>
    /// Visualiza as configurãções da aplicação.
    /// </summary>
    /// <response code="200">Variáveis existentes.</response>
    /// <response code="401">Não autorizado.</response>
    /// <response code="403">Não possui permissão.</response>
    /// <response code="423">Operação temporáriamente indisponível.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpGet("check"), MapToApiVersion("1.0")]
    public IActionResult Host()
    {
        try
        {
            var visualizar = Environment.GetEnvironmentVariable("DATA_VARIAVEIS_VISIVEIS");
            if(!string.IsNullOrEmpty(visualizar) && DateTime.TryParse(visualizar, out _) && DateTime.Now < DateTime.Parse(visualizar))
            {
                return Ok(
                    new HostResponse() 
                    { 
                        UrlDB = Environment.GetEnvironmentVariable("URL_DB_WebAPIPedidos"), 
                        DataVisualizacaoVariaveis = visualizar, 
                        TempoToken = WebAPIPedido.TEMPO_EM_SEGUNDOS_TOKEN, 
                        SecretKey = WebAPIPedido.SECRET_KEY,
                        CertificateName = Environment.GetEnvironmentVariable("CERTIFICATE_NAME"),
                        CertificatePassword = Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD"),
                        Certificate = Environment.GetEnvironmentVariable("CERTIFICATE"),
                        UrlAuthorize = Environment.GetEnvironmentVariable("URL_AUTHORIZE")
                    });
            }
            else
                throw new ProblemaException(StatusCodes.Status423Locked, "Operação temporáriamente indisponível");
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Host", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemaResponse() { Codigo = StatusCodes.Status500InternalServerError, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }
}
