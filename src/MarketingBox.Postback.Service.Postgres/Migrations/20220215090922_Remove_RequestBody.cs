using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Remove_RequestBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBody",
                schema: "postback-service",
                table: "eventreferencelog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                schema: "postback-service",
                table: "eventreferencelog",
                type: "text",
                nullable: true);
        }
    }
}
