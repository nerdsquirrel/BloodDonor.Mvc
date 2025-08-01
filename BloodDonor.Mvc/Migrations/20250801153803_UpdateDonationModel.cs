using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonor.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorEntityId",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "BloodDonorEntityId",
                table: "Donations",
                newName: "CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_BloodDonorEntityId",
                table: "Donations",
                newName: "IX_Donations_CampaignId");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BloodDonorId",
                table: "Donations",
                column: "BloodDonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorId",
                table: "Donations",
                column: "BloodDonorId",
                principalTable: "BloodDonors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_BloodDonorId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Donations",
                newName: "BloodDonorEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_CampaignId",
                table: "Donations",
                newName: "IX_Donations_BloodDonorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorEntityId",
                table: "Donations",
                column: "BloodDonorEntityId",
                principalTable: "BloodDonors",
                principalColumn: "Id");
        }
    }
}
