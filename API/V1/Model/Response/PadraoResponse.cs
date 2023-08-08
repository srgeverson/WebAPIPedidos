namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de retornos genérico padrão da aplicação
    /// </summary>
    public class PadraoResponse
    {
        private IList<string> _mensagens;

        public PadraoResponse()
        {
            _mensagens = new List<string>();
        }
        /// <summary>
        /// Lista de mensagem das operações realizadas
        /// </summary>
        public IList<string> Mensagens { get => _mensagens; set => _mensagens = value; }
    }
}
