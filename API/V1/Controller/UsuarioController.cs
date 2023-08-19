using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.ExceptionHandler;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Facade;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;
    private IUsuarioMapper _usuarioMapper;
    public UsuarioController(IUsuarioService autenticacaoFacade, IUsuarioMapper usuarioMapper)
    {
        _usuarioService = autenticacaoFacade;
        _usuarioMapper = usuarioMapper;
    }

    #region Documentação
    /// <summary>
    /// Apagar usuário por CNPJ.
    /// </summary>
    /// <response code="200">Usuario encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Usuario não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> ApagarPorId([FromQuery] long? id)
    {
        try
        {
            if (id.HasValue)
            {
                var fornecedorExistente = await _usuarioService.BuscarPorId(id);
                if (fornecedorExistente == null)
                    throw new ProblemaException(404, String.Format("Usuario com CNPJ = {0} não foi encontrado!", id));
                else
                {
                    await _usuarioService.Apagar(fornecedorExistente);

                    return Ok(new PadraoResponse() { Mensagens = new List<string>() { "Usuario apagado com sucesso!" } });
                }
            }
            else
                throw new ProblemaException(400, String.Format("CNPJ = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao apagar usuário", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemaResponse() { Codigo = 500, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Atualizar usuário.
    /// </summary>
    /// <response code="200">Usuario encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Usuario não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> AtualizarFornecedor([FromQuery] long? cnpj, [FromBody] FornecedorRequest request)
    {
        try
        {
            if (cnpj.HasValue)
            {
                var usuário = await _usuarioService.BuscarPorId(cnpj);
                if (usuário == null)
                    throw new ProblemaException(404, String.Format("Usuario com ID = {0} não foi encontrado!", cnpj));
                else
                {
                    var fornecedorNovo = _usuarioMapper.ToEntity(request);
                    //fornecedorNovo.Cnpj = cnpj;
                    var fornecedorAtualizado = await _usuarioService.Atualizar(fornecedorNovo);
                    return Ok(fornecedorAtualizado);
                }
            }
            else
                throw new ProblemaException(400, String.Format("CNPJ = {0} inválido!", cnpj));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar usuário", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemaResponse() { Codigo = 500, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Lista todos usuários cadastrados.
    /// </summary>
    /// <response code="200">Todos usuários encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(IList<FornecedorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpGet("todos"), MapToApiVersion("1.0")]
    public async Task<IActionResult> Fornecedores()
    {
        try
        {
            var lista = await _usuarioService.ListarTodos();
            return Ok(lista);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos usuário", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemaResponse() { Codigo = 500, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Buscar usuário por ID.
    /// </summary>
    /// <response code="200">Usuario encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Usuario não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpGet("por-cnpj"), MapToApiVersion("1.0")]
    public async Task<IActionResult> FornecedorPorId([FromQuery] long? cnpj)
    {
        try
        {
            if (cnpj.HasValue)
            {
                var usuário = await _usuarioService.BuscarPorId(cnpj);
                if (usuário == null)
                    throw new ProblemaException(404, String.Format("Usuario com ID = {0} não foi encontrado!", cnpj));
                else
                    return Ok(usuário);
            }
            else
                throw new ProblemaException(400, String.Format("CNPJ = {0} inválido!", cnpj));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar usuário por id", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemaResponse() { Codigo = 500, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Cadastrar usuário.
    /// </summary>
    /// <response code="201">Usuario encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Usuario duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> SalvarFornecedor([FromBody] FornecedorRequest? request)
    {
        try
        {
            if (request == null)
                throw new ProblemaException(400, "Dados não informado!");
            var cnpjExistente = await _usuarioService.BuscarPorId(request.Cnpj);
            if (cnpjExistente != null)
                throw new ProblemaException(409, "Usuario com o CNPJ informado já está cadastrado!");
            var fornecedorNovo = _usuarioMapper.ToEntity(request);
            var fornecedorCadastrado = await _usuarioService.Salvar(fornecedorNovo);
            return Ok(fornecedorCadastrado);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar usuário", Descricao = pex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemaResponse() { Codigo = 500, Mensagem = "Ocorreu um erro interno, tente novamente se o problema persistir contate o administrador do sistema", Descricao = ex.Message });
        }
    }
}

