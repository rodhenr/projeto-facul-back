using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site_Vendas_Fake_API.Database.Migrations
{
    /// <inheritdoc />
    public partial class addpedidopagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoTotal",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pedidos",
                newName: "SituacaoPedido");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaEntrega",
                table: "Pedidos",
                type: "DECIMAL(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PedidoPagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PedidoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FormaPagamento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SituacaoPagamento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoPagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoPagamentos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPagamentos_PedidoId",
                table: "PedidoPagamentos",
                column: "PedidoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoPagamentos");

            migrationBuilder.DropColumn(
                name: "TaxaEntrega",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "SituacaoPedido",
                table: "Pedidos",
                newName: "Status");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoTotal",
                table: "Pedidos",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
