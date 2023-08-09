using Microsoft.EntityFrameworkCore;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IGenericRepository<T, P> where T : class
{
    Task<IEnumerable<T>> SelectAll();
    Task<T> SelectById(P id);
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}
public class GenericRepository<T, P> : IGenericRepository<T, P> where T : class
{
    private ContextRepository _contextRepository = null;
    public GenericRepository(ContextRepository context)
    {
        _contextRepository = context;
    }

    public async Task<IEnumerable<T>> SelectAll()
    {
        return await _contextRepository.Set<T>().AsNoTracking().ToListAsync();
    }
    public async Task<T> SelectById(P id)
    {
        var entity = await _contextRepository.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        var entityUpdated = _contextRepository.Set<T>().Update(entity).Entity;
        await _contextRepository.SaveChangesAsync();
        return entityUpdated;
    }

    public async Task Delete(T entity)
    {
        _contextRepository.Set<T>().Remove(entity);
        await _contextRepository.SaveChangesAsync();
    }

    public async Task<T> Insert(T entity)
    {
        var entityInserted = _contextRepository.Set<T>().Add(entity).Entity;
        await _contextRepository.SaveChangesAsync();
        return entityInserted;
    }
}
