namespace WebAPIPedidos.API.V1.Model.Request;

/// <summary>
/// Representação de um produto a ser processado pela aplicação
/// </summary>
public class ProdutoRequest
{
    private string? _descricao;
    private DateTime? _dataCadastro;
    private decimal? _valor;
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
