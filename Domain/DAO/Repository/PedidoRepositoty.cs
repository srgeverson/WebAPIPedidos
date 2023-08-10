using Microsoft.EntityFrameworkCore;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IPedidoRepositoty : IGenericRepository<PedidoEntity, PedidoId>
{
    Task<IList<PedidoEntity>> SelectById(int? codigoPedido);
    Task<int?> SelectMaxCodigoPedido();
}
public class PedidoRepositoty : GenericRepository<PedidoEntity, PedidoId>, IPedidoRepositoty
{
    public PedidoRepositoty(ContextRepository repositoryContext) : base(repositoryContext) { }

    public async Task<IList<PedidoEntity>> SelectById(int? codigoPedido)
    {
        return await ContextRepository.Set<PedidoEntity>().Where(p=>p.CodigoPedido == codigoPedido).ToListAsync();
    }

    public async Task<int?> SelectMaxCodigoPedido()
    {
        var ultimoCodigo = await ContextRepository.Set<PedidoEntity>().MaxAsync(p => p.CodigoPedido);
        return ultimoCodigo;
    }
}
