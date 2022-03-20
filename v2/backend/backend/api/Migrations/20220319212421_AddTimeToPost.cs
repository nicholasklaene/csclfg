using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class AddTimeToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "posts",
                newName: "is_active");

            migrationBuilder.AddColumn<long>(
                name: "created_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647725061017L);

            migrationBuilder.AddColumn<long>(
                name: "updated_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647725061017L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "posts",
                newName: "IsActive");
        }
    }
}
