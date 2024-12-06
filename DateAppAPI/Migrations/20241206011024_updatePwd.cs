using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatePwd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PwdHash",
                table: "AppUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PwdSalt",
                table: "AppUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PwdHash",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PwdSalt",
                table: "AppUsers");
        }
    }
}
