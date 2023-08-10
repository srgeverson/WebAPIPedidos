using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIPedidos.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    Cnpj = table.Column<long>(type: "bigint", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailContato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeContato = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.Cnpj);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    CodigoPedido = table.Column<int>(type: "int", nullable: false),
                    Fornecedor = table.Column<long>(type: "bigint", nullable: false),
                    Produto = table.Column<int>(type: "int", nullable: false),
                    QuantidadeProduto = table.Column<int>(type: "int", nullable: true),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => new { x.CodigoPedido, x.Fornecedor, x.Produto });
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Codigo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "produtos");
        }
    }
}
