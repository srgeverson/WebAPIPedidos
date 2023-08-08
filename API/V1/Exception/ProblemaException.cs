namespace WebAPIPedidos.API.V1.Exception
{
    /// <summary>
    /// Representação genérica de um erro ocorrido na aplicação
    /// </summary>
    public class ProblemaException : HttpRequestException
    {
        private int _id;

        public ProblemaException() { }
        public ProblemaException(int id)
        {
            _id = id;
        }

        public ProblemaException(string? message) : base(message) { }

        public ProblemaException(int id, string? message) : base(message)
        {
            _id = id;
        }
        /// <summary>
        /// Número de identificação do problema
        /// </summary>
        public int Id { get => _id; set => _id = value; }
    }
}
