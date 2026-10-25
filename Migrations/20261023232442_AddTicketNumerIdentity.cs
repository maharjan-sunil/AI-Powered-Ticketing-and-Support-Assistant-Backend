
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Powered_Ticketing_and_Support_System__Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketNumerIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tickets_TicketId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_AuthorUserId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TicketId",
                table: "Comments",
                newName: "IX_Comments_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorUserId",
                table: "Comments",
                newName: "IX_Comments_AuthorUserId");

            // Replace the existing TicketNumber column with an identity column.
            migrationBuilder.DropColumn(
                name: "TicketNumber",
                table: "Tickets");

            migrationBuilder.AddColumn<long>(
                name: "TicketNumber",
                table: "Tickets",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorUserId",
                table: "Comments",
                column: "AuthorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorUserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketId",
                table: "Comment",
                newName: "IX_Comment_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorUserId",
                table: "Comment",
                newName: "IX_Comment_AuthorUserId");

            // Recreate TicketNumber as a regular bigint column on rollback.
            migrationBuilder.DropColumn(
                name: "TicketNumber",
                table: "Tickets");

            migrationBuilder.AddColumn<long>(
                name: "TicketNumber",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tickets_TicketId",
                table: "Comment",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_AuthorUserId",
                table: "Comment",
                column: "AuthorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
