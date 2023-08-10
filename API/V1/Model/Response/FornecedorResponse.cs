using System.ComponentModel;

namespace WebAPIPedidos.API.V1.Model.Response;
/// <summary>
/// Representação de um fornecedor
/// </summary>
public class FornecedorResponse
{
    private long? _cnpj;
    private string? _uf;
    private string? _emailContato;
    private string? _nomeContato;

    /// <summary>
    /// CNPJ do fornecedor
    /// </summary>
    [DefaultValue(12123123123412)]
    public long? Cnpj { get => _cnpj; set => _cnpj = value; }
    /// <summary>
    /// UF do forncedor
    /// </summary>
    [DefaultValue("CE")]
    public string? Uf { get => _uf; set => _uf = value; }
    /// <summary>
    /// E-mail de contado do forncedor
    /// </summary>
    [DefaultValue("contato@fornecedor.com")]
    public string? EmailContato { get => _emailContato; set => _emailContato = value; }
    /// <summary>
    /// Nome a qual o contato pertence
    /// </summary>
    [DefaultValue("Juze Souza")]
    public string? NomeContato { get => _nomeContato; set => _nomeContato = value; }
}
