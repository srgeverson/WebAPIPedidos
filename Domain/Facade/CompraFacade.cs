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
        var pedidosCadastratos = new List<PedidoEntity>();
        foreach (var item in pedidos)
        {
            var pedidoCadastrado = await _pedidoService.Atualizar(item);
            pedidosCadastratos.Add(pedidoCadastrado);
        }
        return pedidosCadastratos;
    }

    public async Task<IList<PedidoEntity>> CadastrarPedido(IList<PedidoEntity> pedidos)
    {
        var pedidosCadastratos = new List<PedidoEntity>();
        var ultimoCodigoPedido = await _pedidoService.UltimoCodigoPedido();
        ProdutoEntity produto;
        FornecedorEntity fornecedor;
        //PedidoEntity pedidoCadastrado;
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
            if (fornecedor != null || produto != null)
            {
                item.CodigoPedido = ultimoCodigoPedido;
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
