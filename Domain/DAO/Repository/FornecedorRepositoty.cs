using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IFornecedorRepositoty: IGenericRepository<FornecedorEntity, long?> { }
public class FornecedorRepositoty : GenericRepository<FornecedorEntity, long?>, IFornecedorRepositoty
{
    public FornecedorRepositoty(ContextRepository repositoryContext) : base(repositoryContext) { }
}
