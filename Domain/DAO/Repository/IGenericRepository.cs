using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository
{
    public interface IGenericRepository<T, P>
    {
        Task<T> Insert(T entity);
        Task<T> Delete(T entity);
        Task<IList<T>> SelectAll();
        Task<T> GetById(P pk);
        Task<T> Update(T entity);
    }
}
