using Microsoft.EntityFrameworkCore.Migrations;

namespace Phd.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AcademicDepartment_AcademicDepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AcademicDepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AcademicDepartmentId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademicDepartmentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademicDepartmentId",
                table: "AspNetUsers",
                column: "AcademicDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AcademicDepartment_AcademicDepartmentId",
                table: "AspNetUsers",
                column: "AcademicDepartmentId",
                principalTable: "AcademicDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
