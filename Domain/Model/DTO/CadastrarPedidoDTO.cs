using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.Domain.Model.DTO
{
    public class CadastrarPedidoDTO
    {
        private int? _codigoPedido;
        private IList<PedidoEntity> _pedidosParaCadastrar;
        private IList<string> _criticas;

        public IList<PedidoEntity> PedidosParaCadastrar { get => _pedidosParaCadastrar; set => _pedidosParaCadastrar = value; }
        public IList<string> Criticas { get => _criticas; set => _criticas = value; }
        public int? CodigoPedido { get => _codigoPedido; set => _codigoPedido = value; }
    }
}
