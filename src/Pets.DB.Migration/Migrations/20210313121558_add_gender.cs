using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class add_gender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Pet",
                maxLength: 64,
                nullable: false,
                defaultValue: "Unset");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Pet");
        }
    }
}
