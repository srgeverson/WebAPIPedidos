﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIPedidos.Domain.Model.Entity;

[Table("fornecedores")]
public class FornecedorEntity
{
    private long? _cnpj;
    private string? _uf;
    private string? _emailContato;
    private string? _nomeContato;

    [Key]
    public long? Cnpj { get => _cnpj; set => _cnpj = value; }
    public string? Uf { get => _uf; set => _uf = value; }
    public string? EmailContato { get => _emailContato; set => _emailContato = value; }
    public string? NomeContato { get => _nomeContato; set => _nomeContato = value; }
}
