using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class fixmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_affiliatereferencelog_affiliates_AffiliateId",
                schema: "postback-service",
                table: "affiliatereferencelog");

            migrationBuilder.RenameColumn(
                name: "AffiliateId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_affiliatereferencelog_AffiliateId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                newName: "IX_affiliatereferencelog_UserId");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                schema: "postback-service",
                table: "reference",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_affiliatereferencelog_affiliates_UserId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "UserId",
                principalSchema: "postback-service",
                principalTable: "affiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_affiliatereferencelog_affiliates_UserId",
                schema: "postback-service",
                table: "affiliatereferencelog");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "postback-service",
                table: "reference");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                newName: "AffiliateId");

            migrationBuilder.RenameIndex(
                name: "IX_affiliatereferencelog_UserId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                newName: "IX_affiliatereferencelog_AffiliateId");

            migrationBuilder.AddForeignKey(
                name: "FK_affiliatereferencelog_affiliates_AffiliateId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "AffiliateId",
                principalSchema: "postback-service",
                principalTable: "affiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
