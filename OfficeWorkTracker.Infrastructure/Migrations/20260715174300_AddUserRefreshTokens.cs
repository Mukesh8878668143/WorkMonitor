using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeWorkTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRefereshTokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RefereshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserRefereshTokenid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefereshTokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRefereshTokens_UserRefereshTokens_UserRefereshTokenid",
                        column: x => x.UserRefereshTokenid,
                        principalTable: "UserRefereshTokens",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRefereshTokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRefereshTokens_RefereshToken",
                table: "UserRefereshTokens",
                column: "RefereshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefereshTokens_UserID",
                table: "UserRefereshTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefereshTokens_UserRefereshTokenid",
                table: "UserRefereshTokens",
                column: "UserRefereshTokenid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefereshTokens");
        }
    }
}
