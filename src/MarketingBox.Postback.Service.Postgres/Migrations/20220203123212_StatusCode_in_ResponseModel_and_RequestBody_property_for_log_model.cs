using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class StatusCode_in_ResponseModel_and_RequestBody_property_for_log_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                schema: "postback-service",
                table: "eventreferencelog",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBody",
                schema: "postback-service",
                table: "eventreferencelog");
        }
    }
}
