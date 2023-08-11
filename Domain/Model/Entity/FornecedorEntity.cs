using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIPedidos.Domain.Model.Entity;

[Table("fornecedores")]
public class FornecedorEntity
{
    private long? _cnpj;
    private string? _uf;
    private string? _razaoSocial;
    private string? _emailContato;
    private string? _nomeContato;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long? Cnpj { get => _cnpj; set => _cnpj = value; }
    public string? Uf { get => _uf; set => _uf = value; }
    public string? EmailContato { get => _emailContato; set => _emailContato = value; }
    public string? NomeContato { get => _nomeContato; set => _nomeContato = value; }
    public string? RazaoSocial { get => _razaoSocial; set => _razaoSocial = value; }
}
