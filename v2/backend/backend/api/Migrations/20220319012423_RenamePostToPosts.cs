using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class RenamePostToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_categories_category_id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_post_users_user_id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_post_has_tag_post_post_id",
                table: "post_has_tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post",
                table: "post");

            migrationBuilder.RenameTable(
                name: "post",
                newName: "posts");

            migrationBuilder.RenameIndex(
                name: "IX_post_user_id",
                table: "posts",
                newName: "IX_posts_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_category_id",
                table: "posts",
                newName: "IX_posts_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_has_tag_posts_post_id",
                table: "post_has_tag",
                column: "post_id",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_category_id",
                table: "posts",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_user_id",
                table: "posts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_has_tag_posts_post_id",
                table: "post_has_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_categories_category_id",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_user_id",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "post");

            migrationBuilder.RenameIndex(
                name: "IX_posts_user_id",
                table: "post",
                newName: "IX_post_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_posts_category_id",
                table: "post",
                newName: "IX_post_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post",
                table: "post",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_categories_category_id",
                table: "post",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_users_user_id",
                table: "post",
                column: "user_id",
                principalTable: "users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_has_tag_post_post_id",
                table: "post_has_tag",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
