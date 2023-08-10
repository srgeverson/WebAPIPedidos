using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Principal;
using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Facade;
using WebAPIPedidos.Domain.Model.Entity;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class PedidoController : ControllerBase
{
    private ICompraFacade _compraFacade;
    private IPedidoMapper _pedidoMapper;

    public PedidoController(ICompraFacade compraFacade, IPedidoMapper pedidoMapper)
    {
        _compraFacade = compraFacade;
        _pedidoMapper = pedidoMapper;
    }

    /// <summary>
    /// Apagar Pedido por ID.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApagarPorId([FromQuery] PedidoIdRequest id)
    {
        try
        {
            if (id.CodigoPedido.HasValue || id.Produto.HasValue || id.Fornecedor.HasValue)
            {
                var idEntity = _pedidoMapper.ToListIdEntity(id);
                await _compraFacade.ExcluirPedido(idEntity);

                return Ok(new PadraoResponse() { Mensagens = new List<string>() { "Pedido apagado com sucesso!" } });
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao apagar Pedido", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Atualizar Pedido.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarPedido([FromQuery] PedidoIdRequest id, [FromBody] PedidoLoteRequest request)
    {
        try
        {
            if (id.CodigoPedido.HasValue || id.Produto.HasValue || id.Fornecedor.HasValue)
            {
                var idEntity = _pedidoMapper.ToListIdEntity(id);
                var produtoExistente = await _compraFacade.ConsultarPedido(idEntity);
                if (produtoExistente == null)
                    throw new ProblemaException(404, String.Format("Pedido com ID = {0} não foi encontrado!", id));
                else
                {
                    var produtoNovo = _pedidoMapper.ToListEntity(request);
                    produtoNovo.ToList().ForEach(p => p.CodigoPedido = id.CodigoPedido);
                    var produtoAtualizado = await _compraFacade.AlterarPedido(produtoNovo);
                    return Ok(produtoAtualizado);
                }
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar Pedido", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Lista todos produtoes cadastrados.
    /// </summary>
    /// <response code="200">Todos produtoes encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("todos"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IList<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Pedidoes()
    {
        try
        {
            var lista = await _compraFacade.ListarTodosPedidos();
            return Ok(lista);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Pedido", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Buscar Pedido por ID.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("por-id"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PedidoPorId([FromQuery] PedidoIdRequest id)
    {
        try
        {
            if (id.CodigoPedido.HasValue || id.Produto.HasValue || id.Fornecedor.HasValue)
            {
                var idEntity = _pedidoMapper.ToListIdEntity(id);
                var produtoExistente = await _compraFacade.ConsultarPedido(idEntity);
                if (produtoExistente == null)
                    throw new ProblemaException(404, String.Format("Pedido com ID = {0} não foi encontrado!", id));
                else
                    return Ok(produtoExistente);
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar Pedido por id", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Cadastrar Pedido.
    /// </summary>
    /// <response code="201">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Pedido duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SalvarPedido([FromBody] PedidoLoteRequest request)
    {
        try
        {
            var produtoNovo = _pedidoMapper.ToListEntity(request);
            var produtoCadastrado = await _compraFacade.CadastrarPedido(produtoNovo);
            return Ok(produtoCadastrado);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar Pedido", Descricao = pex.Message });
        }
    }
}
