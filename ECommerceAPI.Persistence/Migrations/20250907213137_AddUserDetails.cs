using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "GoogleAuths");

            migrationBuilder.RenameColumn(
                name: "GoogleId",
                table: "GoogleAuths",
                newName: "GoogleIdToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GoogleAuths",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "GoogleAuths",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoogleAuths_UserId1",
                table: "GoogleAuths",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GoogleAuths_AspNetUsers_UserId1",
                table: "GoogleAuths",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoogleAuths_AspNetUsers_UserId1",
                table: "GoogleAuths");

            migrationBuilder.DropIndex(
                name: "IX_GoogleAuths_UserId1",
                table: "GoogleAuths");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GoogleAuths");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "GoogleAuths");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "GoogleIdToken",
                table: "GoogleAuths",
                newName: "GoogleId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GoogleAuths",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
