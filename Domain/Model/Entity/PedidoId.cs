namespace WebAPIPedidos.Domain.Model.Entity
{
    public class PedidoId
    {
        private int? _codigoPedido;
        private long? _fornecedor;
        private int? _produto;

        public int? CodigoPedido { get => _codigoPedido; set => _codigoPedido = value; }
        public long? Fornecedor { get => _fornecedor; set => _fornecedor = value; }
        public int? Produto { get => _produto; set => _produto = value; }
    }
}
