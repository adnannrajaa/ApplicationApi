using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationApi.DataAccess.Migrations
{
    public partial class AllowPagesTabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLinks",
                columns: table => new
                {
                    ActionLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiControllerName = table.Column<string>(nullable: true),
                    ApiControllerUrl = table.Column<string>(nullable: true),
                    PosLinkTitle = table.Column<string>(nullable: true),
                    PosLinkIcon = table.Column<string>(nullable: true),
                    PosLinkColor = table.Column<string>(nullable: true),
                    PosLinkUrl = table.Column<string>(nullable: true),
                    IsActiveLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLinks", x => x.ActionLinkId);
                });

            migrationBuilder.CreateTable(
                name: "AllowedLinks",
                columns: table => new
                {
                    AllowedLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionLinkId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsAssinged = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedLinks", x => x.AllowedLinkId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLinks");

            migrationBuilder.DropTable(
                name: "AllowedLinks");
        }
    }
}
