namespace WebAPIPedidos.API.V1.Model.Response
{
    /// <summary>
    /// Representação de configuração da aplicação
    /// </summary>
    public class HostResponse
    {
        private string? _urlDB;
        private string? _dataVisualizacaoVariaveis;
        /// <summary>
        /// URL de conexão da aplicação
        /// </summary>
        public string? UrlDB { get => _urlDB; set => _urlDB = value; }
        /// <summary>
        /// Data de visualização das variáveis
        /// </summary>
        public string? DataVisualizacaoVariaveis { get => _dataVisualizacaoVariaveis; set => _dataVisualizacaoVariaveis = value; }
    }
}
