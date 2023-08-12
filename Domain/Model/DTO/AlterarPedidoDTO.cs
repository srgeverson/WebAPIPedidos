using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Model.DTO
{
    public class AlterarPedidoDTO
    {
        private IList<PedidoEntity> _pedidosParaCadastrar;
        private IList<PedidoEntity> _pedidosParaAlterar;
        private IList<PedidoEntity> _pedidosParaExcluir;
        private IList<string> _criticas;

        public IList<PedidoEntity> PedidosParaCadastrar { get => _pedidosParaCadastrar; set => _pedidosParaCadastrar = value; }
        public IList<PedidoEntity> PedidosParaAlterar { get => _pedidosParaAlterar; set => _pedidosParaAlterar = value; }
        public IList<PedidoEntity> PedidosParaExcluir { get => _pedidosParaExcluir; set => _pedidosParaExcluir = value; }
        public IList<string> Criticas { get => _criticas; set => _criticas = value; }
    }
}
