using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAPIPedidos.Domain.Model.Entity
{
    public class PedidoIdRequest
    {
        private int? _codigoPedido;
        private long? _fornecedor;
        private int? _produto;
        /// <summary>
        /// Código do produto
        /// </summary>
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(12)]
        public int? CodigoPedido { get => _codigoPedido; set => _codigoPedido = value; }
        /// <summary>
        /// CNPJ do Fornecedor
        /// </summary>
        [DefaultValue(04253011000234)]
        public long? Fornecedor { get => _fornecedor; set => _fornecedor = value; }
        /// <summary>
        /// Código do produto
        /// </summary>
        [DefaultValue(12)]
        public int? Produto { get => _produto; set => _produto = value; }
    }
}
