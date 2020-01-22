using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationApi.DataAccess.Migrations
{
    public partial class addfeildtoUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "currentDateTime",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentDateTime",
                table: "AspNetUsers");
        }
    }
}
