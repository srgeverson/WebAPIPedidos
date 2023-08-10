namespace WebAPIPedidos.API.V1.Model.Request
{
    /// <summary>
    /// Representação de um lote de pedido a ser processado
    /// </summary>
    public class PedidoLoteRequest
    {
        private IList<PedidoRequest> _pedidoRequests;
        /// <summary>
        /// Representação dos itens a qual pertencem ao pedido
        /// </summary>
        public IList<PedidoRequest> PedidoRequests { get => _pedidoRequests; set => _pedidoRequests = value; }
    }
}
