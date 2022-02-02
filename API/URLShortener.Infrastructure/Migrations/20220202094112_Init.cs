using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URLShortener.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortURLVersion = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LongURLVersion = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HitCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_URLs_LongURLVersion",
                table: "URLs",
                column: "LongURLVersion",
                unique: true,
                filter: "[LongURLVersion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_URLs_ShortURLVersion",
                table: "URLs",
                column: "ShortURLVersion",
                unique: true,
                filter: "[ShortURLVersion] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URLs");
        }
    }
}
