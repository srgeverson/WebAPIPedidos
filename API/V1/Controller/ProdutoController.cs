using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProdutoController : ControllerBase
{
    private IProdutoService _produtoService;
    private IProdutoMapper _produtoMapper;

    public ProdutoController(IProdutoService produtoService, IProdutoMapper produtoMapper)
    {
        _produtoService = produtoService;
        _produtoMapper = produtoMapper;
    }

    /// <summary>
    /// Apagar Produto por ID.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApagarPorId([FromQuery] int? id)
    {
        try
        {
            if (id.HasValue)
            {
                var produtoExistente = await _produtoService.BuscarPorId((int)id);
                if (produtoExistente == null)
                    throw new ProblemaException(404, String.Format("Produto com ID = {0} não foi encontrado!", id));
                else
                {
                    await _produtoService.Apagar(produtoExistente);

                    return Ok(new PadraoResponse() { Mensagens = new List<string>() { "Produto apagado com sucesso!" } });
                }
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao apagar Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Atualizar Produto.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarProduto([FromQuery] int? id, [FromBody] ProdutoRequest request)
    {
        try
        {
            if (id.HasValue)
            {
                var Produto = await _produtoService.BuscarPorId((int)id);
                if (Produto == null)
                    throw new ProblemaException(404, String.Format("Produto com ID = {0} não foi encontrado!", id));
                else
                {
                    var produtoNovo = _produtoMapper.ToEntity(request);
                    var produtoAtualizado = await _produtoService.Atualizar(Produto);
                    return Ok(Produto);
                }
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Lista todos produtoes cadastrados.
    /// </summary>
    /// <response code="200">Todos produtoes encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("todos"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IList<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Produtoes()
    {
        try
        {
            var lista = await _produtoService.ListarTodos();
            return Ok(lista);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Buscar Produto por ID.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("por-id"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProdutoPorId([FromQuery] int? id)
    {
        try
        {
            if (id.HasValue)
            {
                var Produto = await _produtoService.BuscarPorId((int)id);
                if (Produto == null)
                    throw new ProblemaException(404, String.Format("Produto com ID = {0} não foi encontrado!", id));
                else
                    return Ok(Produto);
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar Produto por id", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Cadastrar Produto.
    /// </summary>
    /// <response code="201">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Produto duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SalvarProduto([FromBody] ProdutoRequest request)
    {
        try
        {
            var produtoNovo = _produtoMapper.ToEntity(request);
            var produtoCadastrado = await _produtoService.Salvar(produtoNovo);
            return Ok(produtoCadastrado);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar Produto", Descricao = pex.Message });
        }
    }
}
