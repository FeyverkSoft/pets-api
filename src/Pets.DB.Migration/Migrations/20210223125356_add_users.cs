using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class add_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Resource",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Pet",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Page",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "OrganisationContact",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "News",
                maxLength: 1024,
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "varchar(1024) CHARACTER SET utf8mb4",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "News",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Need",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImgLinks",
                table: "Need",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "mediumtext CHARACTER SET utf8mb4",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 512, nullable: true),
                    Permissions = table.Column<string>(maxLength: 2048, nullable: false),
                    State = table.Column<string>(maxLength: 64, nullable: false),
                    ConcurrencyToken = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Resource",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Pet",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Page",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "OrganisationContact",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "News",
                type: "varchar(1024) CHARACTER SET utf8mb4",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldDefaultValue: "[]");

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "News",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 10240,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MdBody",
                table: "Need",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImgLinks",
                table: "Need",
                type: "mediumtext CHARACTER SET utf8mb4",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
