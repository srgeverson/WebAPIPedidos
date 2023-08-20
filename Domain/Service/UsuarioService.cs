using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Service
{
    public interface IUsuarioService : IGenericService<UsuarioEntity, long?>
    {
        Task<UsuarioEntity> BuscarPorLogin(string login);
    }

    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepositoty _produtoRepositoty;

        public UsuarioService(IUsuarioRepositoty UsuarioRepositoty)
        {
            _produtoRepositoty = UsuarioRepositoty;
        }

        public async Task<UsuarioEntity> Apagar(UsuarioEntity model)
        {
            await _produtoRepositoty.Delete(model);
            return model;
        }

        public async Task<UsuarioEntity> Atualizar(UsuarioEntity model)
        {
            return await _produtoRepositoty.Update(model);
        }

        public async Task<UsuarioEntity> BuscarPorId(long? id)
        {
            return await _produtoRepositoty.SelectById(id);
        }

        public async Task<UsuarioEntity> BuscarPorLogin(string login)
        {
            return await _produtoRepositoty.BuscarPorLogin(login);
        }

        public async Task<IList<UsuarioEntity>> ListarTodos()
        {
            var list = await _produtoRepositoty.SelectAll();
            return list.ToList();
        }

        public async Task<UsuarioEntity> Salvar(UsuarioEntity model)
        {
            if (model.DataCadastro == null)
                model.DataCadastro = DateTime.Now;
            var producoCadastrado = await _produtoRepositoty.Insert(model);
            return producoCadastrado;
        }
    }
}
