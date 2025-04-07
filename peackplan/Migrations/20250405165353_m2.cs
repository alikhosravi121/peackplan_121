using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peackplan.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamWorks_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamWorkEntityUserEntity",
                columns: table => new
                {
                    TeamWorksId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamWorkEntityUserEntity", x => new { x.TeamWorksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TeamWorkEntityUserEntity_TeamWorks_TeamWorksId",
                        column: x => x.TeamWorksId,
                        principalTable: "TeamWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamWorkEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamWorkEntityUserEntity_UsersId",
                table: "TeamWorkEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamWorks_CompanyId",
                table: "TeamWorks",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamWorkEntityUserEntity");

            migrationBuilder.DropTable(
                name: "TeamWorks");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
