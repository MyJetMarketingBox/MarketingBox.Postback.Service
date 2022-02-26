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
                name: "affiliates",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affiliates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "affiliatereferencelog",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affiliatereferencelog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_affiliatereferencelog_affiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalSchema: "postback-service",
                        principalTable: "affiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventreferencelog",
                schema: "postback-service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AffiliateId = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationUId = table.Column<string>(type: "text", nullable: true),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    HttpQueryType = table.Column<int>(type: "integer", nullable: false),
                    EventMessage = table.Column<string>(type: "text", nullable: true),
                    PostbackReference = table.Column<string>(type: "text", nullable: true),
                    PostbackResponse = table.Column<string>(type: "text", nullable: true),
                    PostbackResponseStatus = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventreferencelog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventreferencelog_affiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalSchema: "postback-service",
                        principalTable: "affiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_reference_affiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalSchema: "postback-service",
                        principalTable: "affiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_eventreferencelog_PostbackResponseStatus",
                schema: "postback-service",
                table: "eventreferencelog",
                column: "PostbackResponseStatus");

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

            migrationBuilder.DropTable(
                name: "affiliates",
                schema: "postback-service");
        }
    }
}
