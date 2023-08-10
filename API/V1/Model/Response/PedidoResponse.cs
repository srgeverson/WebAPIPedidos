using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de um pedido processado pela aplicação
    /// </summary>
    public class PedidoResponse
    {
        private long? _fornecedor;
        private int? _produto;
        private int? _quantidadeProduto;
        private DateTime? _dataPedido;
        private decimal? _valorPedido;
        /// <summary>
        /// CNPJ do Fornecedor
        /// </summary>
        public long? Fornecedor { get => _fornecedor; set => _fornecedor = value; }
        /// <summary>
        /// Código do produto
        /// </summary>
        public int? Produto { get => _produto; set => _produto = value; }
        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public int? QuantidadeProduto { get => _quantidadeProduto; set => _quantidadeProduto = value; }
        /// <summary>
        /// Data de processamento do pedido
        /// </summary>
        public DateTime? DataPedido { get => _dataPedido; set => _dataPedido = value; }
        /// <summary>
        /// Valor do pedido
        /// </summary>
        public decimal? ValorPedido { get => _valorPedido; set => _valorPedido = value; }
    }
}
