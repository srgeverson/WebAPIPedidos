
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPIPedidos.API.V1.Model.Request
{
    /// <summary>
    /// Representação de um fornecedor a ser processado
    /// </summary>
    public class FornecedorRequest
    {
        private long? _cnpj;
        private string? _razaoSocial;
        private string? _uf;
        private string? _emailContato;
        private string? _nomeContato;

        /// <summary>
        /// CNPJ do fornecedor
        /// </summary>
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(12123123123412)]
        public long? Cnpj { get => _cnpj; set => _cnpj = value; }
        /// <summary>
        /// Nome de registro da empresa
        /// </summary>
        [DefaultValue("Alimentos LTDA")]
        public string? RazaoSocial { get => _razaoSocial; set => _razaoSocial = value; }
        /// <summary>
        /// UF do forncedor
        /// </summary>
        [DefaultValue("CE")]
        public string? Uf { get => _uf; set => _uf = value; }
        /// <summary>
        /// E-mail de contado do forncedor
        /// </summary>
        [DefaultValue("contato@fornecedor.com")]
        public string? EmailContato { get => _emailContato; set => _emailContato = value; }
        /// <summary>
        /// Nome a qual o contato pertence
        /// </summary>
        [DefaultValue("Juze Souza")]
        public string? NomeContato { get => _nomeContato; set => _nomeContato = value; }
    }
}
