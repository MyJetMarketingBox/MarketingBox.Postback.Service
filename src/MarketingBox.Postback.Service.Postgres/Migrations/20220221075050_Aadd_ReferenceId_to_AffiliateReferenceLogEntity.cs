using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Aadd_ReferenceId_to_AffiliateReferenceLogEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_affiliatereferencelog_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "ReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_affiliatereferencelog_reference_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "ReferenceId",
                principalSchema: "postback-service",
                principalTable: "reference",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_affiliatereferencelog_reference_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog");

            migrationBuilder.DropIndex(
                name: "IX_affiliatereferencelog_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog");
        }
    }
}
