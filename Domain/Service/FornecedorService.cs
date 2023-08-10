using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Service
{
    public interface IFornecedorService: IGenericService<FornecedorEntity, long?> { }

    public class FornecedorService : IFornecedorService
    {
        private IFornecedorRepositoty _fornecedorRepositoty;

        public FornecedorService(IFornecedorRepositoty fornecedorRepositoty)
        {
            _fornecedorRepositoty = fornecedorRepositoty;
        }

        public async Task<FornecedorEntity> Apagar(FornecedorEntity model)
        {
            await _fornecedorRepositoty.Delete(model);
            return model;
        }

        public async Task<FornecedorEntity> Atualizar(FornecedorEntity model)
        {
            return await _fornecedorRepositoty.Update(model);
        }

        public async Task<FornecedorEntity> BuscarPorId(long? id)
        {
            return await _fornecedorRepositoty.SelectById(id);
        }

        public async Task<IList<FornecedorEntity>> ListarTodos()
        {
            var list = await _fornecedorRepositoty.SelectAll();
            return list.ToList();
        }

        public async Task<FornecedorEntity> Salvar(FornecedorEntity model)
        {
            await _fornecedorRepositoty.Insert(model);
            return model;
        }
    }
}
