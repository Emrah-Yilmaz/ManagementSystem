using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRelated",
                table: "TestRelated");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TestRelated",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TestRelated",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TestRelated",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TestRelated",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TestRelated",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "TestRelated",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TestRelated",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TestRelated",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRelated",
                table: "TestRelated",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TestRelated_UserId",
                table: "TestRelated",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRelated",
                table: "TestRelated");

            migrationBuilder.DropIndex(
                name: "IX_TestRelated_UserId",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TestRelated");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TestRelated");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRelated",
                table: "TestRelated",
                columns: new[] { "UserId", "ProjectId" });
        }
    }
}
