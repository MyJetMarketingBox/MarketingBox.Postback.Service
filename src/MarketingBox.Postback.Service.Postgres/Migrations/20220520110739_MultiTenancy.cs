using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class MultiTenancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "postback-service",
                table: "reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "postback-service",
                table: "eventreferencelog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "postback-service",
                table: "affiliates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "postback-service",
                table: "reference");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "postback-service",
                table: "eventreferencelog");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "postback-service",
                table: "affiliates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "postback-service",
                table: "affiliatereferencelog");
        }
    }
}
