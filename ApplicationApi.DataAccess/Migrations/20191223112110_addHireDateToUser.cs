using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationApi.DataAccess.Migrations
{
    public partial class addHireDateToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "HireDate",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TargetId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
