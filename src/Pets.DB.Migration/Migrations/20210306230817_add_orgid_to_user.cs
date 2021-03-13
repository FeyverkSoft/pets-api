using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class add_orgid_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "User",
                nullable: false,
                defaultValue: new Guid("10000000-0000-4000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganisationId",
                table: "User",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_OrganisationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "User");
        }
    }
}
