using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsOrderWebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PEDIDO = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ID_PEDIDO",
                table: "ItemPedido",
                column: "ID_PEDIDO");

            migrationBuilder.Sql(@"
                CREATE TRIGGER TRG_UpdateOrderTotal
                ON ItemPedido
                AFTER INSERT, UPDATE, DELETE
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE [Pedido]
                    SET VALOR_TOTAL = (
                        SELECT ISNULL(SUM(PRECO), 0) 
                        FROM ItemPedido 
                        WHERE ID_PEDIDO = [Pedido].Id
                    )
                    WHERE Id IN (
                        SELECT ID_PEDIDO FROM inserted 
                        UNION 
                        SELECT ID_PEDIDO FROM deleted
                    );
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TRG_UpdateOrderTotal");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
