using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTaskManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class KeyChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupInvite",
                table: "GroupInvite");

            migrationBuilder.DropIndex(
                name: "IX_GroupInvite_ReceiverId",
                table: "GroupInvite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupInvite",
                table: "GroupInvite",
                columns: new[] { "ReceiverId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_SenderId",
                table: "GroupInvite",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupInvite",
                table: "GroupInvite");

            migrationBuilder.DropIndex(
                name: "IX_GroupInvite_SenderId",
                table: "GroupInvite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupInvite",
                table: "GroupInvite",
                columns: new[] { "SenderId", "ReceiverId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_ReceiverId",
                table: "GroupInvite",
                column: "ReceiverId");
        }
    }
}
