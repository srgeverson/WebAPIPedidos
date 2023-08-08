using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IFornecedorRepositoty: IGenericRepository<FornecedorEntity, int> { }
public class FornecedorRepositoty : IFornecedorRepositoty
{
    public async Task<FornecedorEntity> Delete(FornecedorEntity entity)
    {
        throw new ProblemaException(423, "Endpoint não implementado ainda");
    }

    public async Task<FornecedorEntity> GetById(int pk)
    {
        throw new ProblemaException(423, "Endpoint não implementado ainda");
    }

    public async Task<FornecedorEntity> Insert(FornecedorEntity entity)
    {
        throw new ProblemaException(423, "Endpoint não implementado ainda");
    }

    public async Task<IList<FornecedorEntity>> SelectAll()
    {
        throw new ProblemaException(423, "Endpoint não implementado ainda");
    }

    public async Task<FornecedorEntity> Update(FornecedorEntity entity)
    {
        throw new ProblemaException(423, "Endpoint não implementado ainda");
    }
}
