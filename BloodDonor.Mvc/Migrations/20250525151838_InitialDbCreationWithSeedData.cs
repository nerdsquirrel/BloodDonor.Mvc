using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonor.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreationWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodDonors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodGroup = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodDonorId = table.Column<int>(type: "int", nullable: false),
                    BloodDonorEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_BloodDonors_BloodDonorEntityId",
                        column: x => x.BloodDonorEntityId,
                        principalTable: "BloodDonors",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BloodDonors",
                columns: new[] { "Id", "Address", "BloodGroup", "ContactNumber", "DateOfBirth", "Email", "FullName", "LastDonationDate", "ProfilePicture", "Weight" },
                values: new object[,]
                {
                    { 1, "New York", 0, "9876543210", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice@example.com", "Alice Thomas", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "profiles/alice.jpg", 60f },
                    { 2, "Chicago", 7, "9876543211", new DateTime(1985, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@example.com", "Bob Smith", null, "profiles/bob.jpg", 72f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BloodDonorEntityId",
                table: "Donations",
                column: "BloodDonorEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "BloodDonors");
        }
    }
}
