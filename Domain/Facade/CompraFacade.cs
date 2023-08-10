using System.Security.Principal;
using WebAPIPedidos.API.V1.ExceptionHandler;
using WebAPIPedidos.Domain.Model.Entity;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.Domain.Facade;
public interface ICompraFacade
{
    Task<IList<PedidoEntity>> AlterarPedido(IList<PedidoEntity> pedidos);
    Task<FornecedorEntity> BuscarFornecedorPorId(long? fornecedor);
    Task<IList<PedidoEntity>> BuscarPedidoPorId(PedidoId pedidoId);
    Task<ProdutoEntity> BuscarProdutoPorId(int? produto);
    Task<IList<PedidoEntity>> CadastrarPedido(IList<PedidoEntity> pedidos);
    Task<IList<PedidoEntity>> ExcluirPedido(PedidoId id);
    Task<IList<PedidoEntity>> ExcluirPedido(IList<PedidoEntity> pedidos);
    Task<IList<PedidoEntity>> ListarTodosPedidos();
    Task<int> UltimoCodigoPedido();
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
        foreach (var item in pedidos)
        {
            var pedidoCadastrado = await _pedidoService.Atualizar(item);
            pedidosAlteradoos.Add(pedidoCadastrado);
        }
        return pedidosAlteradoos;
    }

    public async Task<FornecedorEntity?> BuscarFornecedorPorId(long? fornecedor)
    {
        return await _fornecedorService.BuscarPorId(fornecedor);
    }

    public async Task<ProdutoEntity?> BuscarProdutoPorId(int? produto)
    {
        return await _produtoService.BuscarPorId((int)produto);
    }

    public async Task<IList<PedidoEntity>> CadastrarPedido(IList<PedidoEntity> pedidos)
    {
        var pedidosCadastratos = new List<PedidoEntity>();
        foreach (var item in pedidos)
        {
            pedidosCadastratos.Add(await _pedidoService.Salvar(item));
        }
        return pedidosCadastratos;
    }

    public async Task<IList<PedidoEntity>> BuscarPedidoPorId(PedidoId id)
    {
        var pedidos = new List<PedidoEntity>();
        if (id.Produto.HasValue || id.Fornecedor.HasValue)
        {
            var pedido = await _pedidoService.BuscarPorId(id);
            if (pedido != null)
                pedidos.Add(pedido);
        }
        else
        {
            var pedidosPorCodigo = await _pedidoService.BuscarPorId(id.CodigoPedido);
            pedidos = pedidosPorCodigo.ToList();
        }
        return pedidos;
    }

    public async Task<IList<PedidoEntity>> ExcluirPedido(PedidoId id)
    {
        var pedidos = await BuscarPedidoPorId(id);
        if (pedidos == null)
            throw new ProblemaException(404, String.Format("Pedido com ID = {0} não foi encontrado!", id));
        else
            foreach (var item in pedidos)
            {
                await _pedidoService.Apagar(item);
            }
        return pedidos;
    }

    public async Task<IList<PedidoEntity>> ListarTodosPedidos()
    {
       return await _pedidoService.ListarTodos();
    }

    public async Task<int> UltimoCodigoPedido()
    {
        return await _pedidoService.UltimoCodigoPedido();
    }

    public async Task<IList<PedidoEntity>> ExcluirPedido(IList<PedidoEntity> pedidos)
    {
        foreach (var item in pedidos)
        {
            await _pedidoService.Apagar(item);
        }
        return pedidos;
    }
}
