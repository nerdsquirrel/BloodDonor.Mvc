using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonor.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureInBloodDonorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_BloodDonorId",
                table: "Donations");

            migrationBuilder.AddColumn<int>(
                name: "BloodDonorEntityId",
                table: "Donations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "BloodDonors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BloodDonorEntityId",
                table: "Donations",
                column: "BloodDonorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorEntityId",
                table: "Donations",
                column: "BloodDonorEntityId",
                principalTable: "BloodDonors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_BloodDonors_BloodDonorEntityId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_BloodDonorEntityId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "BloodDonorEntityId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "BloodDonors");

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
        }
    }
}
