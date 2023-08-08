using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Service
{
    public interface IFornecedorService: IGenericService<FornecedorEntity, int> { }

    public class FornecedorService : IFornecedorService
    {
        private IFornecedorRepositoty _fornecedorRepositoty;

        public FornecedorService(IFornecedorRepositoty fornecedorRepositoty)
        {
            _fornecedorRepositoty = fornecedorRepositoty;
        }

        public async Task<FornecedorEntity> Apagar(FornecedorEntity model)
        {
            return await _fornecedorRepositoty.Delete(model);
        }

        public async Task<FornecedorEntity> Atualizar(FornecedorEntity model)
        {
            return await _fornecedorRepositoty.Update(model);
        }

        public async Task<FornecedorEntity> BuscarPorId(int id)
        {
            return await _fornecedorRepositoty.GetById(id);
        }

        public async Task<IList<FornecedorEntity>> ListarTodos()
        {
            return await _fornecedorRepositoty.SelectAll();
        }

        public async Task<FornecedorEntity> Salvar(FornecedorEntity model)
        {
            return await _fornecedorRepositoty.Insert(model);
        }
    }
}
