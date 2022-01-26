using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Added_AffiliateId_To_EventReferenceLog_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AffiliateId",
                schema: "postback-service",
                table: "eventreferencelog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_AffiliateId",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "AffiliateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_eventreferencelog_AffiliateId",
                schema: "postback-service",
                table: "eventreferencelog");

            migrationBuilder.DropColumn(
                name: "AffiliateId",
                schema: "postback-service",
                table: "eventreferencelog");
        }
    }
}
