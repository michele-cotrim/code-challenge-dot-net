using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuaveChallenge.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "CheckinInformation",
                type: "int",
                nullable: false, // Por causa do IsRequired() na sua configuração
                defaultValue: 0); // EF Core adiciona um valor padrão para colunas não anuláveis

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
                onDelete: ReferentialAction.Restrict); // Ou o comportamento de exclusão que você definiu
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
