using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class TryFixFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "label",
                table: "tags",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "updated_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647737453873L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647725061017L);

            migrationBuilder.AlterColumn<long>(
                name: "created_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647737453873L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647725061017L);

            migrationBuilder.AlterColumn<string>(
                name: "tag_label",
                table: "post_has_tag",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "tag_label",
                table: "category_has_suggested_tag",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "label",
                table: "tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "updated_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647725061017L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647737453873L);

            migrationBuilder.AlterColumn<long>(
                name: "created_at",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 1647725061017L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 1647737453873L);

            migrationBuilder.AlterColumn<string>(
                name: "tag_label",
                table: "post_has_tag",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "tag_label",
                table: "category_has_suggested_tag",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");
        }
    }
}
