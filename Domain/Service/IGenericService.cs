namespace WebAPIPedidos.Domain.Service
{
    public interface IGenericService<T, P>
    {
        Task<T> Apagar(T model);
        Task<T> Atualizar(T model);
        Task<T> BuscarPorId(P id);
        Task<IList<T>> ListarTodos();
        Task<T> Salvar(T model);
    }
}
