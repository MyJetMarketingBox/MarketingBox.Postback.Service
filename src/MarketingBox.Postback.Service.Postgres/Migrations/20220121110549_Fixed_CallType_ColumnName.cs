using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    public partial class Fixed_CallType_ColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallType",
                schema: "postback-service",
                table: "reference",
                newName: "HttpQueryType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HttpQueryType",
                schema: "postback-service",
                table: "reference",
                newName: "CallType");
        }
    }
}
