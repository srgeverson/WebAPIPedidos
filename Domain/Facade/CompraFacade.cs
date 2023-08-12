using WebAPIPedidos.Domain.Model.DTO;
using WebAPIPedidos.Domain.Model.Entity;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.Domain.Facade;
public interface ICompraFacade
{
    Task<AlterarPedidoDTO> AlterarPedido(IList<PedidoEntity> pedidos, PedidoId id);
    Task<FornecedorEntity> BuscarFornecedorPorId(long? fornecedor);
    Task<IList<PedidoEntity>> BuscarPedidoPorId(PedidoId pedidoId);
    Task<ProdutoEntity> BuscarProdutoPorId(int? produto);
    Task<CadastrarPedidoDTO> CadastrarPedido(IList<PedidoEntity> pedidos);
    Task<IList<PedidoEntity>> ExcluirPedido(IList<PedidoEntity> pedidos);
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

    public async Task<AlterarPedidoDTO> AlterarPedido(IList<PedidoEntity> pedidos, PedidoId id)
    {
        var alterarPedidoDTO =  new AlterarPedidoDTO();
        alterarPedidoDTO.PedidosParaCadastrar = new List<PedidoEntity>();
        alterarPedidoDTO.PedidosParaAlterar = new List<PedidoEntity>();
        alterarPedidoDTO.PedidosParaExcluir = new List<PedidoEntity>();
        alterarPedidoDTO.Criticas = new List<string>();
        foreach (var item in pedidos)
        {
            ProdutoEntity produto = null;
            FornecedorEntity fornecedor = null;
            if (item.Produto.HasValue)
                produto = await this.BuscarProdutoPorId(item.Produto);
            if (item.Fornecedor.HasValue)
                fornecedor = await this.BuscarFornecedorPorId(item.Fornecedor);
            if (fornecedor != null && produto != null)
            {
                item.CodigoPedido = id.CodigoPedido;
                item.DataPedido = DateTime.Now;
                item.ValorPedido = item.QuantidadeProduto * produto.Valor;
                var produtoExistente = await this.BuscarPedidoPorId(new PedidoId() { CodigoPedido = item.CodigoPedido, Fornecedor = item.Fornecedor, Produto = item.Produto });
                if (!produtoExistente.Any())
                {
                    var itemAdicionado = await _pedidoService.Salvar(item);
                    alterarPedidoDTO.PedidosParaCadastrar.Add(itemAdicionado);
                }
                else
                {
                    if (item.ValorPedido > 0)
                    {
                        var itemAlterado = await _pedidoService.Atualizar(item);
                        alterarPedidoDTO.PedidosParaAlterar.Add(itemAlterado);
                    }
                    else
                    {
                        var itemExcluido = await _pedidoService.Apagar(item);
                        alterarPedidoDTO.PedidosParaExcluir.Add(itemExcluido);
                    }
                }
            }
            else
            {
                if (fornecedor == null)
                    alterarPedidoDTO.Criticas.Add(string.Concat("O fornecedor ", item.Fornecedor, " não foi processado pois não foi encontrado"));
                if (produto == null)
                    alterarPedidoDTO.Criticas.Add(string.Concat("O produto ", item.Produto, " não foi processado pois não foi encontrado"));
            }
        }
        return alterarPedidoDTO;
    }

    public async Task<FornecedorEntity?> BuscarFornecedorPorId(long? fornecedor)
    {
        return await _fornecedorService.BuscarPorId(fornecedor);
    }

    public async Task<ProdutoEntity?> BuscarProdutoPorId(int? produto)
    {
        return await _produtoService.BuscarPorId((int)produto);
    }

    public async Task<CadastrarPedidoDTO> CadastrarPedido(IList<PedidoEntity> pedidos)
    {
        var cadastrarPedidoDTO = new CadastrarPedidoDTO();
        cadastrarPedidoDTO.PedidosParaCadastrar = new List<PedidoEntity>();
        cadastrarPedidoDTO.Criticas = new List<string>();
        cadastrarPedidoDTO.CodigoPedido = await this.UltimoCodigoPedido();
        var agrupandoItens = (
            pedidos
            .GroupBy(p => new { CodigoPedido = cadastrarPedidoDTO.CodigoPedido, Produto = p.Produto, Fornecedor = p.Fornecedor })
            .Select(p => new { Id = p.Key, QuantidadeProduto = p.Sum(ps => ps.QuantidadeProduto) })
            .ToList()
            );

        foreach (var item in agrupandoItens)
        {
            var pedido = new PedidoEntity();
            ProdutoEntity produto = null;
            FornecedorEntity fornecedor = null;
            if (item.Id.Produto.HasValue)
                produto = await this.BuscarProdutoPorId(item.Id.Produto);
            if (item.Id.Fornecedor.HasValue)
                fornecedor = await this.BuscarFornecedorPorId(item.Id.Fornecedor);
            if (fornecedor != null && produto != null)
            {
                pedido.CodigoPedido = item.Id.CodigoPedido;
                pedido.Fornecedor = item.Id.Fornecedor;
                pedido.Produto = item.Id.Produto;
                pedido.DataPedido = DateTime.Now;
                pedido.QuantidadeProduto = item.QuantidadeProduto;
                pedido.ValorPedido = item.QuantidadeProduto * produto.Valor;
                var produtoExistente = await this.BuscarPedidoPorId(new PedidoId() { CodigoPedido = pedido.CodigoPedido, Fornecedor = pedido.Fornecedor, Produto = pedido.Produto });
                if (!produtoExistente.Any())
                {
                    var itemAdicionado = await _pedidoService.Salvar(pedido);
                    cadastrarPedidoDTO.PedidosParaCadastrar.Add(itemAdicionado);
                }
                else
                    cadastrarPedidoDTO.Criticas.Add(string.Concat("O produto ", item.Id.Produto, " com fornecedor ", item.Id.Fornecedor, " já consta no pedido ", item.Id.CodigoPedido));
            }
            else
            {
                if (fornecedor == null)
                    cadastrarPedidoDTO.Criticas.Add(string.Concat("O fornecedor ", item.Id.Fornecedor, " não foi processado pois não foi encontrado"));
                if (produto == null)
                    cadastrarPedidoDTO.Criticas.Add(string.Concat("O produto ", item.Id.Produto, " não foi processado pois não foi encontrado"));
            }
        }
        return cadastrarPedidoDTO;
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

    public async Task<IList<PedidoEntity>> ListarTodosPedidos()
    {
       return await _pedidoService.ListarTodos();
    }

    private async Task<int> UltimoCodigoPedido()
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
