using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PivotIt.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "sq_PivotIt_UserMessage_ID");

            migrationBuilder.CreateSequence<int>(
                name: "sq_PivotIt_UserMessageAttachment_ID");

            migrationBuilder.CreateTable(
                name: "PivotIt_AppLog",
                columns: table => new
                {
                    LogID = table.Column<string>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivotIt_AppLog", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "PivotIt_SiteUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PasswordSalt = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivotIt_SiteUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PivotIt_UserMessage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR sq_PivotIt_UserMessage_ID"),
                    UserID = table.Column<string>(nullable: true),
                    CCUsersID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    MessageBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivotIt_UserMessage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PivotIt_UserMessageAttachment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR sq_PivotIt_UserMessageAttachment_ID"),
                    AttachmentPath = table.Column<string>(nullable: true),
                    MessageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivotIt_UserMessageAttachment", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PivotIt_AppLog_LogDate",
                table: "PivotIt_AppLog",
                column: "LogDate");

            migrationBuilder.CreateIndex(
                name: "IX_PivotIt_SiteUser_Email",
                table: "PivotIt_SiteUser",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PivotIt_SiteUser_UserName",
                table: "PivotIt_SiteUser",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PivotIt_UserMessage_UserID",
                table: "PivotIt_UserMessage",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PivotIt_UserMessageAttachment_MessageID",
                table: "PivotIt_UserMessageAttachment",
                column: "MessageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PivotIt_AppLog");

            migrationBuilder.DropTable(
                name: "PivotIt_SiteUser");

            migrationBuilder.DropTable(
                name: "PivotIt_UserMessage");

            migrationBuilder.DropTable(
                name: "PivotIt_UserMessageAttachment");

            migrationBuilder.DropSequence(
                name: "sq_PivotIt_UserMessage_ID");

            migrationBuilder.DropSequence(
                name: "sq_PivotIt_UserMessageAttachment_ID");
        }
    }
}
