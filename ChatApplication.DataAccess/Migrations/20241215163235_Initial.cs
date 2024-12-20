using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    FirstConnectedUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastConnectedUTC = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConnections");
        }
    }
}
