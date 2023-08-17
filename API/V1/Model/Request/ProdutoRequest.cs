using System.ComponentModel;

namespace WebAPIPedidos.API.V1.Model.Request;

/// <summary>
/// Representação de um produto a ser processado pela aplicação
/// </summary>
public class ProdutoRequest
{
    private string? _descricao;
    private decimal? _valor;
    /// <summary>
    /// Descrição do produto
    /// </summary>
    [DefaultValue("Arroz branco 1kg")]
    public string? Descricao { get => _descricao; set => _descricao = value; }
    /// <summary>
    /// Valor do produto
    /// </summary>
    [DefaultValue(1.00)]
    public decimal? Valor { get => _valor; set => _valor = value; }
}
