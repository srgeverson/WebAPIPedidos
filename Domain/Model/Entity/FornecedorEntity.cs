namespace WebAPIPedidos.Domain.Model.Entity;
public class FornecedorEntity
{
    private int? _id;
    private string? _cnpj;
    private string? _uf;
    private string? _emailContato;
    private string? _nomeContato;

    public int? Id { get => _id; set => _id = value; }
    public string? Cnpj { get => _cnpj; set => _cnpj = value; }
    public string? Uf { get => _uf; set => _uf = value; }
    public string? EmailContato { get => _emailContato; set => _emailContato = value; }
    public string? NomeContato { get => _nomeContato; set => _nomeContato = value; }
}
