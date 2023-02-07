using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.API.Infrastructure.Migrations
{
    public partial class AddLoyaltyPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LoyaltyPoints",
                schema: "ordering",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                schema: "ordering",
                table: "orders");
        }
    }
}
