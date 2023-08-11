using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIPedidos.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "fornecedores",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "fornecedores");
        }
    }
}
