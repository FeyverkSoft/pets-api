using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    BeforePhotoLink = table.Column<string>(maxLength: 512, nullable: true),
                    AfterPhotoLink = table.Column<string>(maxLength: 512, nullable: true),
                    PetState = table.Column<string>(maxLength: 64, nullable: false),
                    MdBody = table.Column<string>(maxLength: 10240, nullable: true),
                    Type = table.Column<string>(maxLength: 64, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetState",
                table: "Pet",
                column: "PetState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pet");
        }
    }
}
