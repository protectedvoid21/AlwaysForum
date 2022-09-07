using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class FKCommentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReports_Comments_CommentId",
                table: "CommentReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentUpVotes_Comments_CommentId",
                table: "CommentUpVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReports_Comments_CommentId",
                table: "CommentReports",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentUpVotes_Comments_CommentId",
                table: "CommentUpVotes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReports_Comments_CommentId",
                table: "CommentReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentUpVotes_Comments_CommentId",
                table: "CommentUpVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReports_Comments_CommentId",
                table: "CommentReports",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentUpVotes_Comments_CommentId",
                table: "CommentUpVotes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
