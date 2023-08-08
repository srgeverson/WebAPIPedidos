namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de um problema ocorrido na aplicação
    /// </summary>
    public class ProblemaResponse
    {
        private int? _codigo;
        private string? _mensagem;
        private string? _descricao;

        /// <summary>
        /// Número de identificação do problema
        /// </summary>
        public int? Codigo { get => _codigo; set => _codigo = value; }
        /// <summary>
        /// Mensagem do problema ocorrido
        /// </summary>
        public string? Mensagem { get => _mensagem; set => _mensagem = value; }

        /// <summary>
        /// Descrição do problema ocorrido
        /// </summary>
        public string? Descricao { get => _descricao; set => _descricao = value; }
    }
}
