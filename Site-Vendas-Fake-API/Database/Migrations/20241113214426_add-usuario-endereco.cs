using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site_Vendas_Fake_API.Database.Migrations
{
    /// <inheritdoc />
    public partial class addusuarioendereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UsuarioEnderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEnderecos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioEnderecos");

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
