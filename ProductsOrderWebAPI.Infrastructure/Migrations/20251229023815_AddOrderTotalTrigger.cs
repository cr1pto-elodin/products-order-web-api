using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsOrderWebAPI.Infrastructure.Migrations
{
    public partial class AddOrderTotalTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER TRG_UpdateOrderTotal
                ON ItemPedido
                AFTER INSERT, UPDATE, DELETE
                AS
                BEGIN
                    UPDATE [Pedido]
                    SET VALOR_TOTAL = (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE ID_PEDIDO = [Pedido].Id)
                    WHERE Id IN (SELECT ID_PEDIDO FROM Product);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TRG_UpdateOrderTotal");
        }
    }
}