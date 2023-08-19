namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de configuração da aplicação
    /// </summary>
    public class HostResponse
    {
        private string? _urlDB;
        private string? _dataVisualizacaoVariaveis;
        private Double? _tempoToken;
        private byte[]? _secretKey;
        /// <summary>
        /// URL de conexão da aplicação
        /// </summary>
        public string? UrlDB { get => _urlDB; set => _urlDB = value; }
        /// <summary>
        /// Data de visualização das variáveis
        /// </summary>
        public string? DataVisualizacaoVariaveis { get => _dataVisualizacaoVariaveis; set => _dataVisualizacaoVariaveis = value; }
        /// <summary>
        /// Tempo configurado de expiração do token
        /// </summary>
        public double? TempoToken { get => _tempoToken; set => _tempoToken = value; }
        /// <summary>
        /// Chave secreta do token
        /// </summary>
        public byte[]? SecretKey { get => _secretKey; set => _secretKey = value; }
    }
}
