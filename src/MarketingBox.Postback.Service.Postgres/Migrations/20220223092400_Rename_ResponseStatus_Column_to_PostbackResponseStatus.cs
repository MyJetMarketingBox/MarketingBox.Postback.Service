using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Rename_ResponseStatus_Column_to_PostbackResponseStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                newName: "PostbackResponseStatus");

            migrationBuilder.RenameIndex(
                name: "IX_eventreferencelog_ResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                newName: "IX_eventreferencelog_PostbackResponseStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostbackResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                newName: "ResponseStatus");

            migrationBuilder.RenameIndex(
                name: "IX_eventreferencelog_PostbackResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                newName: "IX_eventreferencelog_ResponseStatus");
        }
    }
}
