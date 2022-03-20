using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class TryFixManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "updated_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647738841208L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647737453873L);

            migrationBuilder.AlterColumn<long>(
                name: "created_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647738841208L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647737453873L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "updated_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647737453873L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647738841208L);

            migrationBuilder.AlterColumn<long>(
                name: "created_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647737453873L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647738841208L);
        }
    }
}
