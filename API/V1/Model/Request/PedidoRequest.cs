using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPIPedidos.API.V1.Model.Request
{
    /// <summary>
    /// Representação de um pedido a ser processado
    /// </summary>
    public class PedidoRequest
    {
        private int? _produto;
        private int? _quantidade;
        private long? fornecedor;
        /// <summary>
        /// Código do produto
        /// </summary>
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(12)]
        public int? Produto { get => _produto; set => _produto = value; }
        /// <summary>
        /// Quantidade do produto
        /// </summary>
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(10)]
        public int? QuantidadeProduto { get => _quantidade; set => _quantidade = value; }
        /// <summary>
        /// CNPJ do Fornecedor
        /// </summary>
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(04253011000234)]
        public long? Fornecedor { get => fornecedor; set => fornecedor = value; }
    }
}
