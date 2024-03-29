using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_UserId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Task",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_UserId",
                table: "Task",
                newName: "IX_Task_AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_AssignedUserId",
                table: "Task",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_AssignedUserId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "Task",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AssignedUserId",
                table: "Task",
                newName: "IX_Task_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
