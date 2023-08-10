using System.Security.Principal;
using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.Domain.Model.Entity;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.Domain.Facade;
public interface ICompraFacade
{
    Task<IList<PedidoEntity>> AlterarPedido(IList<PedidoEntity> pedidos);
    Task<IList<PedidoEntity>> CadastrarPedido(IList<PedidoEntity> pedidos);
    Task<PedidoEntity> ConsultarPedido(PedidoId id);
    Task<IList<PedidoEntity>> ExcluirPedido(PedidoId id);
    Task<IList<PedidoEntity>> ListarTodosPedidos();
}
public class CompraFacade : ICompraFacade
{
    private IPedidoService _pedidoService;
    private IProdutoService _produtoService;
    private IFornecedorService _fornecedorService;

    public CompraFacade(IPedidoService pedidoService, IProdutoService produtoService, IFornecedorService fornecedorService)
    {
        _pedidoService = pedidoService;
        _produtoService = produtoService;
        _fornecedorService = fornecedorService;
    }

    public async Task<IList<PedidoEntity>> AlterarPedido(IList<PedidoEntity> pedidos)
    {
        var pedidosAlteradoos = new List<PedidoEntity>();
        ProdutoEntity produto;
        FornecedorEntity fornecedor;
        foreach (var item in pedidos)
        {
            if (item.Produto.HasValue)
                produto = await _produtoService.BuscarPorId((int)item.Produto);
            else
                produto = null;
            
            var itemAdicionado = await _pedidoService.BuscarPorId(new PedidoId() { CodigoPedido = item.CodigoPedido, Fornecedor = item.Fornecedor, Produto = item.Produto });
            if (itemAdicionado != null && produto != null)
            {
                item.ValorPedido = item.QuantidadeProduto * produto.Valor;
                var pedidoCadastrado = await _pedidoService.Atualizar(item);
                pedidosAlteradoos.Add(pedidoCadastrado);
            }
            else
                pedidosAlteradoos.Add(item);
        }
        return pedidosAlteradoos;
    }

    public async Task<IList<PedidoEntity>> CadastrarPedido(IList<PedidoEntity> pedidos)
    {
        var pedidosCadastratos = new List<PedidoEntity>();
        var ultimoCodigoPedido = await _pedidoService.UltimoCodigoPedido();
        ProdutoEntity produto;
        FornecedorEntity fornecedor;
        foreach (var item in pedidos)
        {
            if (item.Produto.HasValue)
                produto = await _produtoService.BuscarPorId((int)item.Produto);
            else
                produto = null;
            if (item.Fornecedor.HasValue)
                fornecedor = await _fornecedorService.BuscarPorId((int)item.Fornecedor);
            else
                fornecedor = null;
            var itensAdicionados = await _pedidoService.BuscarPorId(new PedidoId() { CodigoPedido = ultimoCodigoPedido, Fornecedor = item.Fornecedor, Produto = item.Produto });
            if (fornecedor != null && produto != null && itensAdicionados == null)
            {
                item.CodigoPedido = ultimoCodigoPedido;
                item.DataPedido = DateTime.Now;
                item.ValorPedido = item.QuantidadeProduto * produto.Valor;
                var pedidoCadastrado = await _pedidoService.Salvar(item);
                pedidosCadastratos.Add(pedidoCadastrado);
            }
            else
                pedidosCadastratos.Add(item);
        }
        return pedidosCadastratos;
    }

    public async Task<PedidoEntity> ConsultarPedido(PedidoId id)
    {
        return await _pedidoService.BuscarPorId(id);
    }

    public async Task<IList<PedidoEntity>> ExcluirPedido(PedidoId id)
    {
        var produtoExistente = await ConsultarPedido(id);
        if (produtoExistente == null)
            throw new ProblemaException(404, String.Format("Pedido com ID = {0} não foi encontrado!", id));
        else
        {
        }
        throw new NotImplementedException();
    }

    public async Task<IList<PedidoEntity>> ListarTodosPedidos()
    {
       return await _pedidoService.ListarTodos();
    }
}
