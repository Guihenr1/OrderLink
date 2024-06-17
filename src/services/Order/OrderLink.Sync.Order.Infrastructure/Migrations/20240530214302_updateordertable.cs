using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderLink.Sync.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes");

            migrationBuilder.RenameTable(
                name: "Dishes",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Dishes");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Dishes",
                newName: "DishId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes",
                column: "Id");
        }
    }
}
