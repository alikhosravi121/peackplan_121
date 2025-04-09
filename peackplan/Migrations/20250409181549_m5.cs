using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peackplan.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryTaskEntity_OkrEntity_OkrEntityId",
                table: "PrimaryTaskEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryTaskEntity",
                table: "PrimaryTaskEntity");

            migrationBuilder.RenameTable(
                name: "PrimaryTaskEntity",
                newName: "PrimaryTasks");

            migrationBuilder.RenameIndex(
                name: "IX_PrimaryTaskEntity_OkrEntityId",
                table: "PrimaryTasks",
                newName: "IX_PrimaryTasks_OkrEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryTasks",
                table: "PrimaryTasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryTasks_ManagerId",
                table: "PrimaryTasks",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryTasks_OkrEntity_OkrEntityId",
                table: "PrimaryTasks",
                column: "OkrEntityId",
                principalTable: "OkrEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryTasks_Users_ManagerId",
                table: "PrimaryTasks",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryTasks_OkrEntity_OkrEntityId",
                table: "PrimaryTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryTasks_Users_ManagerId",
                table: "PrimaryTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryTasks",
                table: "PrimaryTasks");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryTasks_ManagerId",
                table: "PrimaryTasks");

            migrationBuilder.RenameTable(
                name: "PrimaryTasks",
                newName: "PrimaryTaskEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PrimaryTasks_OkrEntityId",
                table: "PrimaryTaskEntity",
                newName: "IX_PrimaryTaskEntity_OkrEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryTaskEntity",
                table: "PrimaryTaskEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryTaskEntity_OkrEntity_OkrEntityId",
                table: "PrimaryTaskEntity",
                column: "OkrEntityId",
                principalTable: "OkrEntity",
                principalColumn: "Id");
        }
    }
}
