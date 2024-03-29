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
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserRole",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserRole",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "UserRole",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Status",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Status",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Status",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Status",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Comment",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Comment");
        }
    }
}
