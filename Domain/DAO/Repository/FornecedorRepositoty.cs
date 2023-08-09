using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IFornecedorRepositoty: IGenericRepository<FornecedorEntity, int> { }
public class FornecedorRepositoty : GenericRepository<FornecedorEntity, int>, IFornecedorRepositoty
{
    public FornecedorRepositoty(ContextRepository repositoryContext) : base(repositoryContext) { }
}
