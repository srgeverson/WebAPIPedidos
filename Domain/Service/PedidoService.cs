using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Service
{
    public interface IPedidoService : IGenericService<PedidoEntity, PedidoId>
    {
        Task<int> UltimoCodigoPedido();
        Task<IList<PedidoEntity>> BuscarPorId(int? codigoPedido);
    }

    public class PedidoService : IPedidoService
    {
        private IPedidoRepositoty _produtoRepositoty;

        public PedidoService(IPedidoRepositoty pedidoRepositoty)
        {
            _produtoRepositoty = pedidoRepositoty;
        }

        public async Task<PedidoEntity> Apagar(PedidoEntity model)
        {
            await _produtoRepositoty.Delete(model);
            return model;
        }

        public async Task<PedidoEntity> Atualizar(PedidoEntity model)
        {
            return await _produtoRepositoty.Update(model);
        }

        public async Task<PedidoEntity> BuscarPorId(PedidoId id)
        {
            var comCodigoPedido = await BuscarPorId(id.CodigoPedido);
            if (id.Produto.HasValue)
                comCodigoPedido = comCodigoPedido.Where(p => p.Produto.Equals(id.Produto)).ToList();
            if (id.Fornecedor.HasValue)
                comCodigoPedido = comCodigoPedido.Where(p => p.Fornecedor.Equals(id.Fornecedor)).ToList();
            return comCodigoPedido.FirstOrDefault();
        }

        public async Task<IList<PedidoEntity>> ListarTodos()
        {
            var list = await _produtoRepositoty.SelectAll();
            return list.ToList();
        }

        public async Task<PedidoEntity> Salvar(PedidoEntity model)
        {
            var pedidoCadastrado = await _produtoRepositoty.Insert(model);
            return pedidoCadastrado;
        }

        public async Task<int> UltimoCodigoPedido()
        {
            var ultimoCodigo = await _produtoRepositoty.SelectMaxCodigoPedido();
            int numeroPedido = ultimoCodigo != null ? (int)ultimoCodigo + 1 : 1;
            return numeroPedido;
        }

        public async Task<IList<PedidoEntity>> BuscarPorId(int? codigoPedido)
        {
            var comCodigoPedido = await _produtoRepositoty.SelectById(codigoPedido);
            return comCodigoPedido;
        }
    }
}
