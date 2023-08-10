using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de um pedido processado pela aplicação
    /// </summary>
    public class PedidoLoteResponse
    {
        private int? _codigoPedido;
        private IList<PedidoResponse> _itens;
        private IList<string> _mensagens;
        /// <summary>
        /// Identificação de um pedido
        /// </summary>
        public int? CodigoPedido { get => _codigoPedido; set => _codigoPedido = value; }
        /// <summary>
        /// Itens existentes no pedido
        /// </summary>
        public IList<PedidoResponse> Itens { get => _itens; set => _itens = value; }
        /// <summary>
        /// Mensagens informativas do processamento
        /// </summary>
        public IList<string> Mensagens { get => _mensagens; set => _mensagens = value; }
    }
}
