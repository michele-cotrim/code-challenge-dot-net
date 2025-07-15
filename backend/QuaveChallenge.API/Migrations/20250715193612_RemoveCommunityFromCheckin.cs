using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuaveChallenge.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommunityFromCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckinInformation_Communities_CommunityId",
                table: "CheckinInformation");

            migrationBuilder.DropIndex(
                name: "IX_CheckinInformation_CommunityId",
                table: "CheckinInformation");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "CheckinInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "CheckinInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckinInformation_CommunityId",
                table: "CheckinInformation",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckinInformation_Communities_CommunityId",
                table: "CheckinInformation",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
