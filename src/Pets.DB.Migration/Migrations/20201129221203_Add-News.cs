using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class AddNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Tags = table.Column<string>(maxLength: 1024, nullable: false),
                    ImgLink = table.Column<string>(maxLength: 512, nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: false, defaultValue: ""),
                    MdShortBody = table.Column<string>(maxLength: 512, nullable: false),
                    MdBody = table.Column<string>(maxLength: 10240, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => new { x.Id, x.OrganisationId });
                    table.UniqueConstraint("AK_News_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsPets",
                columns: table => new
                {
                    PetId = table.Column<Guid>(nullable: false),
                    NewsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPets", x => new { x.NewsId, x.PetId });
                    table.ForeignKey(
                        name: "FK_NewsPets_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsPets_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_Id",
                table: "News",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_OrganisationId",
                table: "News",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPets_PetId",
                table: "NewsPets",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPets_NewsId_PetId",
                table: "NewsPets",
                columns: new[] { "NewsId", "PetId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPets");

            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
