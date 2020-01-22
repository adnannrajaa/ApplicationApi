using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationApi.DataAccess.Migrations
{
    public partial class updateIsassigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AllowedLinks_ActionLinkId",
                table: "AllowedLinks",
                column: "ActionLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedLinks_ActionLinks_ActionLinkId",
                table: "AllowedLinks",
                column: "ActionLinkId",
                principalTable: "ActionLinks",
                principalColumn: "ActionLinkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedLinks_ActionLinks_ActionLinkId",
                table: "AllowedLinks");

            migrationBuilder.DropIndex(
                name: "IX_AllowedLinks_ActionLinkId",
                table: "AllowedLinks");
        }
    }
}
