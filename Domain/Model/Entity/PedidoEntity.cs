﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIPedidos.Domain.Model.Entity;

[Table("pedidos")]
[PrimaryKey(nameof(CodigoPedido), nameof(Fornecedor), nameof(Produto))]
public class PedidoEntity
{
    private int? _codigoPedido;
    private long? _fornecedor;
    private int? _produto;
    private int? _quantidadeProduto;
    private DateTime? _dataPedido;
    private decimal? _valorPedido;
    public int? CodigoPedido { get => _codigoPedido; set => _codigoPedido = value; }
    public long? Fornecedor { get => _fornecedor; set => _fornecedor = value; }
    public int? Produto { get => _produto; set => _produto = value; }
    public int? QuantidadeProduto { get => _quantidadeProduto; set => _quantidadeProduto = value; }
    public DateTime? DataPedido { get => _dataPedido; set => _dataPedido = value; }
    public decimal? ValorPedido { get => _valorPedido; set => _valorPedido = value; }
}
