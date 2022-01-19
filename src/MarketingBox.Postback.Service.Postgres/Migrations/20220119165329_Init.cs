using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "postback-service");

            migrationBuilder.CreateTable(
                name: "reference",
                schema: "postback-service",
                columns: table => new
                {
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationReference = table.Column<string>(type: "text", nullable: true),
                    RegistrationTGReference = table.Column<string>(type: "text", nullable: true),
                    DepositReference = table.Column<string>(type: "text", nullable: true),
                    DepositTGReference = table.Column<string>(type: "text", nullable: true),
                    CallType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reference", x => x.ReferenceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reference_AffiliateId",
                schema: "postback-service",
                table: "reference",
                column: "AffiliateId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reference",
                schema: "postback-service");
        }
    }
}
