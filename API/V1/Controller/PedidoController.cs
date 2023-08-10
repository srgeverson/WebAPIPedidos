using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Facade;
using WebAPIPedidos.Domain.Model.Entity;

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

    #region Documentação
    /// <summary>
    /// Apagar Pedido por ID.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> ApagarPorCodigoPedido([FromQuery] PedidoIdRequest id)
    {
        try
        {
            if (id.CodigoPedido.HasValue || id.Produto.HasValue || id.Fornecedor.HasValue)
            {
                var idEntity = _pedidoMapper.ToListIdEntity(id);
                var pedido = await _compraFacade.BuscarPedidoPorId(idEntity);
                if (pedido.Count == 0)
                    throw new ProblemaException(404, String.Format("Não há dados para o pedido = {0}, fornecedor = {1} e produto = {2} informado!", id.CodigoPedido,id.Fornecedor,id.Produto));
                await _compraFacade.ExcluirPedido(pedido);

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

    #region Documentação
    /// <summary>
    /// Atualizar Pedido.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> AtualizarPedido([FromQuery] PedidoIdRequest id, [FromBody] PedidoLoteRequest request)
    {
        try
        {
            
            var produtoNovo = _pedidoMapper.ToListEntity(request);
            var pedidosParaCadastrar = new List<PedidoEntity>();
            var pedidosParaAlterar = new List<PedidoEntity>();
            var pedidosParaExcluir = new List<PedidoEntity>();
            var pedidosResponse = new PedidoLoteResponse();
            var mensagens = new List<string>();
            foreach (var item in produtoNovo)
            {
                ProdutoEntity produto = null;
                FornecedorEntity fornecedor = null;
                if (item.Produto.HasValue)
                    produto = await _compraFacade.BuscarProdutoPorId(item.Produto);
                if (item.Fornecedor.HasValue)
                    fornecedor = await _compraFacade.BuscarFornecedorPorId(item.Fornecedor);
                if (fornecedor != null && produto != null)
                {
                    item.CodigoPedido = id.CodigoPedido;
                    item.DataPedido = DateTime.Now;
                    item.ValorPedido = item.QuantidadeProduto * produto.Valor;
                    var produtoExistente = await _compraFacade.BuscarPedidoPorId(new PedidoId() { CodigoPedido = item.CodigoPedido, Fornecedor = item.Fornecedor, Produto = item.Produto });
                    if (!produtoExistente.Any())
                        pedidosParaCadastrar.Add(item);
                    else
                    {
                        if (item.ValorPedido > 0)
                            pedidosParaAlterar.Add(item);
                        else
                            pedidosParaExcluir.Add(item);
                    }
                }
                else
                {
                    if (fornecedor == null)
                        mensagens.Add(string.Concat("O fornecedor ", item.Fornecedor, " não foi processado pois não foi encontrado"));
                    if (produto == null)
                        mensagens.Add(string.Concat("O produto ", item.Produto, " não foi processado pois não foi encontrado"));
                }
            }

            var pedidoAlterado = await _compraFacade.AlterarPedido(pedidosParaAlterar);
            var pedidoCadastrado = await _compraFacade.CadastrarPedido(pedidosParaCadastrar);
            var pedidoExcluido = await _compraFacade.ExcluirPedido(pedidosParaExcluir);
            pedidosResponse.CodigoPedido = id.CodigoPedido;
            mensagens.AddRange(pedidosParaAlterar.Select(p => string.Concat("Produdo ", p.Produto, " alterado no pedido")).ToList());
            mensagens.AddRange(pedidosParaAlterar.Select(p => string.Concat("Produdo ", p.Produto, " incluído no pedido")).ToList());
            mensagens.AddRange(pedidosParaExcluir.Select(p => string.Concat("Produdo ", p.Produto, " removido do pedido")).ToList());
            pedidosResponse.Mensagens = mensagens;
            var pedidosAlterado = new List<PedidoResponse>();
            pedidosAlterado.AddRange(_pedidoMapper.ToListResponse(pedidoAlterado));
            pedidosAlterado.AddRange(_pedidoMapper.ToListResponse(pedidoCadastrado));
            pedidosAlterado.AddRange(_pedidoMapper.ToListResponse(pedidoExcluido));
            pedidosResponse.Itens = pedidosAlterado;
            if (pedidosAlterado.Any())
                return Ok(pedidosResponse);
            else
                throw new ProblemaException(400, string.Join(", ", pedidosResponse.Mensagens));

        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar Pedido", Descricao = pex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Lista todos produtoes cadastrados.
    /// </summary>
    /// <response code="200">Todos produtoes encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(IList<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpGet("todos"), MapToApiVersion("1.0")]
    public async Task<IActionResult> Pedidoes()
    {
        try
        {
            var lista = await _compraFacade.ListarTodosPedidos();
            var agrupadosPorPedido = lista.ToList().GroupBy(p => p.CodigoPedido).ToList();
            var pedidos = new List<PedidoLoteResponse>();
            agrupadosPorPedido.ForEach(p =>
            {
                pedidos.Add(new PedidoLoteResponse() { CodigoPedido = p.Key, Itens = _pedidoMapper.ToListResponse(p.ToList()) });
            });
            return Ok(pedidos);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Pedido", Descricao = pex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Buscar Pedido por ID.
    /// </summary>
    /// <response code="200">Pedido encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Pedido não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PedidoLoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpGet("por-id"), MapToApiVersion("1.0")]
    public async Task<IActionResult> PedidoPorId([FromQuery] PedidoIdRequest id)
    {
        try
        {
            if (id.CodigoPedido.HasValue || id.Produto.HasValue || id.Fornecedor.HasValue)
            {
                var idEntity = _pedidoMapper.ToListIdEntity(id);
                var produtoExistente = await _compraFacade.BuscarPedidoPorId(idEntity);
                var pedidosResponse = new PedidoLoteResponse();
                if (produtoExistente == null)
                    throw new ProblemaException(404, String.Format("Pedido com ID = {0}, produto = {1} e fornecedor = {2} não foi encontrado!", id.CodigoPedido, id.Fornecedor, id.Produto));
                pedidosResponse.CodigoPedido = id.CodigoPedido;
                pedidosResponse.Itens = _pedidoMapper.ToListResponse(produtoExistente);
                return Ok(pedidosResponse);
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar Pedido por id", Descricao = pex.Message });
        }
    }

    #region Documentação
    /// <summary>
    /// Cadastrar Pedido.
    /// </summary>
    /// <response code="201">Pedido cadastrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Pedido duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [ProducesResponseType(typeof(PedidoLoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    #endregion
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    public async Task<IActionResult> SalvarPedido([FromBody] PedidoLoteRequest request)
    {
        try
        {
            var ultimoCodigoPedido = await _compraFacade.UltimoCodigoPedido();
            var produtoNovo = _pedidoMapper.ToListEntity(request);
            var pedidosParaCadastrar = new List<PedidoEntity>();
            var pedidosResponse = new PedidoLoteResponse();
            var mensagens = new List<string>();
            foreach (var item in produtoNovo)
            {
                ProdutoEntity produto = null;
                FornecedorEntity fornecedor = null;
                if (item.Produto.HasValue)
                    produto = await _compraFacade.BuscarProdutoPorId(item.Produto);
                if (item.Fornecedor.HasValue)
                    fornecedor = await _compraFacade.BuscarFornecedorPorId(item.Fornecedor);
                if (fornecedor != null && produto != null)
                {
                    item.CodigoPedido = ultimoCodigoPedido;
                    item.DataPedido = DateTime.Now;
                    item.ValorPedido = item.QuantidadeProduto * produto.Valor;
                    pedidosParaCadastrar.Add(item);
                }
                else
                {
                    if (fornecedor == null)
                        mensagens.Add(string.Concat("O fornecedor ", item.Fornecedor, " não foi processado pois não foi encontrado"));
                    if (produto == null)
                        mensagens.Add(string.Concat("O produto ", item.Produto, " não foi processado pois não foi encontrado"));
                }
            }

            var pedidoCadastrado = await _compraFacade.CadastrarPedido(pedidosParaCadastrar);
            pedidosResponse.CodigoPedido = ultimoCodigoPedido;
            pedidosResponse.Mensagens = mensagens.Any() ? mensagens : pedidoCadastrado.Select(p => string.Concat("Produdo vinculado ", p.Produto, " vinculado ao pedido")).ToList();
            pedidosResponse.Itens = _pedidoMapper.ToListResponse(pedidoCadastrado);
            if (pedidoCadastrado.Any())
                return StatusCode(201, pedidosResponse);
            else
                throw new ProblemaException(400, string.Join(", ", pedidosResponse.Mensagens));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar Pedido", Descricao = pex.Message });
        }
    }
}
