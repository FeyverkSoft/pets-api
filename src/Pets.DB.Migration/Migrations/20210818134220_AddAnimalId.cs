using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class AddAnimalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "User",
                nullable: false,
                defaultValue: new Guid("10000000-0000-4000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<decimal>(
                name: "AnimalId",
                table: "Pet",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Pet");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "User",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("10000000-0000-4000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_User_Organisation_OrganisationId",
                table: "User",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
