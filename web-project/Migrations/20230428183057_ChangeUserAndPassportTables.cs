using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_project.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndPassportTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Users_UserSerialCode",
                table: "DrivingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCards_Users_UserSerialCode",
                table: "StudentCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_StudentCards_UserSerialCode",
                table: "StudentCards");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicences_UserSerialCode",
                table: "DrivingLicences");

            migrationBuilder.DropColumn(
                name: "SerialCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserSerialCode",
                table: "StudentCards");

            migrationBuilder.DropColumn(
                name: "UserSerialCode",
                table: "DrivingLicences");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StudentCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DrivingLicences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCards_UserId",
                table: "StudentCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_UserId",
                table: "DrivingLicences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_UserId",
                table: "Passports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Users_UserId",
                table: "DrivingLicences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCards_Users_UserId",
                table: "StudentCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Users_UserId",
                table: "DrivingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCards_Users_UserId",
                table: "StudentCards");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_StudentCards_UserId",
                table: "StudentCards");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicences_UserId",
                table: "DrivingLicences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentCards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DrivingLicences");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Nationality");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "ImagePath");

            migrationBuilder.AddColumn<string>(
                name: "SerialCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserSerialCode",
                table: "StudentCards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserSerialCode",
                table: "DrivingLicences",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "SerialCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCards_UserSerialCode",
                table: "StudentCards",
                column: "UserSerialCode");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_UserSerialCode",
                table: "DrivingLicences",
                column: "UserSerialCode");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Users_UserSerialCode",
                table: "DrivingLicences",
                column: "UserSerialCode",
                principalTable: "Users",
                principalColumn: "SerialCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCards_Users_UserSerialCode",
                table: "StudentCards",
                column: "UserSerialCode",
                principalTable: "Users",
                principalColumn: "SerialCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
