using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokyBack.Migrations
{
    /// <inheritdoc />
    public partial class ReaddedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Rooms_RoomId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_RoomId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Logs");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Rooms_RoomCode",
                table: "Logs",
                column: "RoomCode",
                principalTable: "Rooms",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Rooms_RoomCode",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Logs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_RoomId",
                table: "Logs",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Rooms_RoomId",
                table: "Logs",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
