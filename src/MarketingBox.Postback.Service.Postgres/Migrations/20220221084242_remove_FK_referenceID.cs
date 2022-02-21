using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class remove_FK_referenceID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_affiliatereferencelog_reference_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog");

            migrationBuilder.DropIndex(
                name: "IX_affiliatereferencelog_ReferenceId",
                schema: "postback-service",
                table: "affiliatereferencelog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
