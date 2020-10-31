using Microsoft.EntityFrameworkCore.Migrations;

namespace Pets.DB.Migrations.Migrations
{
    public partial class AddNeedState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NeedState",
                table: "Need",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedState",
                table: "Need");
        }
    }
}
