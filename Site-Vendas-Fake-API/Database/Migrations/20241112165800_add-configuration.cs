using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site_Vendas_Fake_API.Database.Migrations
{
    /// <inheritdoc />
    public partial class addconfiguration : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCategorias_CategoriaId",
                table: "ProdutoCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCategorias_ProdutoId",
                table: "ProdutoCategorias",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ProdutoId",
                table: "ItemPedidos",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_Pedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_Produtos_ProdutoId",
                table: "ItemPedidos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoCategorias_Categorias_CategoriaId",
                table: "ProdutoCategorias",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoCategorias_Produtos_ProdutoId",
                table: "ProdutoCategorias",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_Pedidos_PedidoId",
                table: "ItemPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_Produtos_ProdutoId",
                table: "ItemPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoCategorias_Categorias_CategoriaId",
                table: "ProdutoCategorias");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoCategorias_Produtos_ProdutoId",
                table: "ProdutoCategorias");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoCategorias_CategoriaId",
                table: "ProdutoCategorias");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoCategorias_ProdutoId",
                table: "ProdutoCategorias");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedidos_ProdutoId",
                table: "ItemPedidos");

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
