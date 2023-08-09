using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIPedidos.Domain.Model.Entity;

[Table("produtos")]
public class ProdutoEntity
{
    private int? _codigo;
    private string? _descricao;
    private DateTime? _dataCadastro;
    private decimal? _valor;

    [Key]
    public int? Codigo { get => _codigo; set => _codigo = value; }
    public string? Descricao { get => _descricao; set => _descricao = value; }
    public DateTime? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
    public decimal? Valor { get => _valor; set => _valor = value; }
}
