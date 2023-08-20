using Microsoft.EntityFrameworkCore;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.DAO.Repository;
public interface IUsuarioRepositoty : IGenericRepository<UsuarioEntity, long?>
{
    Task<UsuarioEntity> BuscarPorLogin(string login);
}
public class UsuarioRepositoty : GenericRepository<UsuarioEntity, long?>, IUsuarioRepositoty
{
    public UsuarioRepositoty(ContextRepository context) : base(context)
    {
    }

    public async Task<UsuarioEntity> BuscarPorLogin(string login)
    {
        var usuarios = await ContextRepository.Usuarios.Where(u => u.Login.Equals(login)).ToListAsync();
        return usuarios.FirstOrDefault();
    }
}
