using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peackplan.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSrc",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TeamWorks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "TeamWorks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "TeamWorks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSrc",
                table: "TeamWorks",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerTeamWrokId",
                table: "TeamWorks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "TeamWorks",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OkrEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AccessLevel = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PriorityWeight = table.Column<float>(type: "real", nullable: false),
                    StartValue = table.Column<int>(type: "integer", nullable: true),
                    CurrentValue = table.Column<int>(type: "integer", nullable: true),
                    GoalValue = table.Column<int>(type: "integer", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ParentOkrId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OkrEntity_OkrEntity_ParentOkrId",
                        column: x => x.ParentOkrId,
                        principalTable: "OkrEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagEntity_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NoteEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OkrEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteEntity_OkrEntity_OkrEntityId",
                        column: x => x.OkrEntityId,
                        principalTable: "OkrEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkrEntityUserEntity",
                columns: table => new
                {
                    OkrsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrEntityUserEntity", x => new { x.OkrsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_OkrEntityUserEntity_OkrEntity_OkrsId",
                        column: x => x.OkrsId,
                        principalTable: "OkrEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryTaskEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AccessLevel = table.Column<int>(type: "integer", nullable: false),
                    ParentTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uuid", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OkrEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryTaskEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryTaskEntity_OkrEntity_OkrEntityId",
                        column: x => x.OkrEntityId,
                        principalTable: "OkrEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    OkrId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_OkrEntity_OkrId",
                        column: x => x.OkrId,
                        principalTable: "OkrEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NoteReceiverEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteReceiverEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteReceiverEntity_NoteEntity_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NoteEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteReceiverEntity_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteEntity_OkrEntityId",
                table: "NoteEntity",
                column: "OkrEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteReceiverEntity_NoteId",
                table: "NoteReceiverEntity",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteReceiverEntity_ReceiverId",
                table: "NoteReceiverEntity",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrEntity_ParentOkrId",
                table: "OkrEntity",
                column: "ParentOkrId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrEntityUserEntity_UsersId",
                table: "OkrEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryTaskEntity_OkrEntityId",
                table: "PrimaryTaskEntity",
                column: "OkrEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_CompanyId",
                table: "TagEntity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_OkrId",
                table: "UploadedFiles",
                column: "OkrId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_UsersId",
                table: "UploadedFiles",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteReceiverEntity");

            migrationBuilder.DropTable(
                name: "OkrEntityUserEntity");

            migrationBuilder.DropTable(
                name: "PrimaryTaskEntity");

            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "NoteEntity");

            migrationBuilder.DropTable(
                name: "OkrEntity");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileSrc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "TeamWorks");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "TeamWorks");

            migrationBuilder.DropColumn(
                name: "FileSrc",
                table: "TeamWorks");

            migrationBuilder.DropColumn(
                name: "OwnerTeamWrokId",
                table: "TeamWorks");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "TeamWorks");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TeamWorks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
