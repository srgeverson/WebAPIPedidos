namespace WebAPIPedidos.API.V1.Model.Response;
/// <summary>
/// Representação de um produto processado pela aplicação
/// </summary>
public class ProdutoResponse
{
    private int? _codigo;
    private string? _descricao;
    private DateTime? _dataCadastro;
    private decimal? _valor;
    /// <summary>
    /// Código do produto
    /// </summary>
    public int? Codigo { get => _codigo; set => _codigo = value; }
    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string? Descricao { get => _descricao; set => _descricao = value; }
    /// <summary>
    /// Data de cadastro do prodto
    /// </summary>
    public DateTime? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
    /// <summary>
    /// Valor do produto
    /// </summary>
    public decimal? Valor { get => _valor; set => _valor = value; }
}
