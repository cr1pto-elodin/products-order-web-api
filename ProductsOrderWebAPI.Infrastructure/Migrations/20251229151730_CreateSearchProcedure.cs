using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsOrderWebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateSearchProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_GetOrdersByTotalPrice
                    @TotalPrice DECIMAL(18,2)
                AS
                BEGIN
                    SELECT * FROM Pedido WHERE VALOR_TOTAL >= @TotalPrice
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE SP_GetOrdersByTotalPrice");
        }
    }
}
