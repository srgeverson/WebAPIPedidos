using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Service
{
    public interface IProdutoService : IGenericService<ProdutoEntity, int> { }

    public class ProdutoService : IProdutoService
    {
        private IProdutoRepositoty _produtoRepositoty;

        public ProdutoService(IProdutoRepositoty ProdutoRepositoty)
        {
            _produtoRepositoty = ProdutoRepositoty;
        }

        public async Task<ProdutoEntity> Apagar(ProdutoEntity model)
        {
            await _produtoRepositoty.Delete(model);
            return model;
        }

        public async Task<ProdutoEntity> Atualizar(ProdutoEntity model)
        {
            return await _produtoRepositoty.Update(model);
        }

        public async Task<ProdutoEntity> BuscarPorId(int id)
        {
            return await _produtoRepositoty.SelectById(id);
        }

        public async Task<IList<ProdutoEntity>> ListarTodos()
        {
            var list = await _produtoRepositoty.SelectAll();
            return list.ToList();
        }

        public async Task<ProdutoEntity> Salvar(ProdutoEntity model)
        {
            var producoCadastrado = await _produtoRepositoty.Insert(model);
            return producoCadastrado;
        }
    }
}
