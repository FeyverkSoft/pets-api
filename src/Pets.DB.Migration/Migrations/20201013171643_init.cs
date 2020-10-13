using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Need",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    ImgLinks = table.Column<string>(maxLength: 2048, nullable: true),
                    MdBody = table.Column<string>(maxLength: 2048, nullable: true),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Need", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Need_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 64, nullable: false),
                    ImgLink = table.Column<string>(maxLength: 512, nullable: true),
                    MdBody = table.Column<string>(maxLength: 2048, nullable: true),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationContact_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    ImgLink = table.Column<string>(maxLength: 512, nullable: true),
                    MdBody = table.Column<string>(maxLength: 10240, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => new { x.Id, x.OrganisationId });
                    table.ForeignKey(
                        name: "FK_Page_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    BeforePhotoLink = table.Column<string>(maxLength: 512, nullable: true),
                    AfterPhotoLink = table.Column<string>(maxLength: 512, nullable: true),
                    PetState = table.Column<string>(maxLength: 64, nullable: false),
                    MdShortBody = table.Column<string>(maxLength: 512, nullable: true),
                    MdBody = table.Column<string>(maxLength: 10240, nullable: true),
                    Type = table.Column<string>(maxLength: 64, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    ImgLink = table.Column<string>(maxLength: 512, nullable: true),
                    MdBody = table.Column<string>(maxLength: 2048, nullable: true),
                    State = table.Column<string>(maxLength: 64, nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Need_OrganisationId",
                table: "Need",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationContact_OrganisationId",
                table: "OrganisationContact",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_OrganisationId",
                table: "Page",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_OrganisationId",
                table: "Pet",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetState",
                table: "Pet",
                column: "PetState");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_OrganisationId",
                table: "Resource",
                column: "OrganisationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Need");

            migrationBuilder.DropTable(
                name: "OrganisationContact");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Organisation");
        }
    }
}
