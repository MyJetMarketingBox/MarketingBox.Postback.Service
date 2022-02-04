using System;
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
                name: "affiliatereferencelog",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affiliatereferencelog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventreferencelog",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    HttpQueryType = table.Column<int>(type: "integer", nullable: false),
                    PostbackReference = table.Column<string>(type: "text", nullable: true),
                    RequestBody = table.Column<string>(type: "text", nullable: true),
                    PostbackResponse = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationUId = table.Column<string>(type: "text", nullable: true),
                    EventMessage = table.Column<string>(type: "text", nullable: true),
                    ResponseStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventreferencelog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reference",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationReference = table.Column<string>(type: "text", nullable: true),
                    RegistrationTGReference = table.Column<string>(type: "text", nullable: true),
                    DepositReference = table.Column<string>(type: "text", nullable: true),
                    DepositTGReference = table.Column<string>(type: "text", nullable: true),
                    HttpQueryType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reference", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_affiliatereferencelog_AffiliateId",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_affiliatereferencelog_Date",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_affiliatereferencelog_Operation",
                schema: "postback-service",
                table: "affiliatereferencelog",
                column: "Operation");

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_AffiliateId",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_Date",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_EventType",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_HttpQueryType",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "HttpQueryType");

            migrationBuilder.CreateIndex(
                name: "IX_eventreferencelog_ResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "ResponseStatus");

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
                name: "affiliatereferencelog",
                schema: "postback-service");

            migrationBuilder.DropTable(
                name: "eventreferencelog",
                schema: "postback-service");

            migrationBuilder.DropTable(
                name: "reference",
                schema: "postback-service");
        }
    }
}
