using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site_Vendas_Fake_API.Database.Migrations
{
    /// <inheritdoc />
    public partial class addenderecos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UF",
                table: "UsuarioEnderecos",
                newName: "Uf");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "UsuarioEnderecos",
                newName: "Cep");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoTotal",
                table: "Pedidos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "UsuarioEnderecos",
                newName: "UF");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "UsuarioEnderecos",
                newName: "CEP");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoTotal",
                table: "Pedidos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
