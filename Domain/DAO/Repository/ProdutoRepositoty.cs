using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IProdutoRepositoty: IGenericRepository<ProdutoEntity, int> { }
public class ProdutoRepositoty : GenericRepository<ProdutoEntity, int>, IProdutoRepositoty
{
    public ProdutoRepositoty(ContextRepository repositoryContext) : base(repositoryContext) { }
}
