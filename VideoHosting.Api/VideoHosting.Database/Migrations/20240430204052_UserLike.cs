using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoHosting.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDislike_AspNetUsers_UserId",
                table: "UserDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDislike_Videos_VideoId",
                table: "UserDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_AspNetUsers_UserId",
                table: "UserLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Videos_VideoId",
                table: "UserLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDislike",
                table: "UserDislike");

            migrationBuilder.RenameTable(
                name: "UserLike",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "UserDislike",
                newName: "Dislikes");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_VideoId",
                table: "Likes",
                newName: "IX_Likes_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDislike_VideoId",
                table: "Dislikes",
                newName: "IX_Dislikes_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "UserId", "VideoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dislikes",
                table: "Dislikes",
                columns: new[] { "UserId", "VideoId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Dislikes_AspNetUsers_UserId",
                table: "Dislikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dislikes_Videos_VideoId",
                table: "Dislikes",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dislikes_AspNetUsers_UserId",
                table: "Dislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Dislikes_Videos_VideoId",
                table: "Dislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dislikes",
                table: "Dislikes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "UserLike");

            migrationBuilder.RenameTable(
                name: "Dislikes",
                newName: "UserDislike");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_VideoId",
                table: "UserLike",
                newName: "IX_UserLike_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Dislikes_VideoId",
                table: "UserDislike",
                newName: "IX_UserDislike_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike",
                columns: new[] { "UserId", "VideoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDislike",
                table: "UserDislike",
                columns: new[] { "UserId", "VideoId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Administrator", "Administrator" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislike_AspNetUsers_UserId",
                table: "UserDislike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislike_Videos_VideoId",
                table: "UserDislike",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_AspNetUsers_UserId",
                table: "UserLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Videos_VideoId",
                table: "UserLike",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
