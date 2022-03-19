using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class ChangeCategoriesColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_applications_ApplicationId",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "categories",
                newName: "application_id");

            migrationBuilder.RenameIndex(
                name: "IX_categories_ApplicationId",
                table: "categories",
                newName: "IX_categories_application_id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_applications_application_id",
                table: "categories",
                column: "application_id",
                principalTable: "applications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_applications_application_id",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "application_id",
                table: "categories",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_application_id",
                table: "categories",
                newName: "IX_categories_ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_applications_ApplicationId",
                table: "categories",
                column: "ApplicationId",
                principalTable: "applications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
