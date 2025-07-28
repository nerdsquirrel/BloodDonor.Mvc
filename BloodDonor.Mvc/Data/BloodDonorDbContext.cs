using BloodDonor.Mvc.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Data
{
    public class BloodDonorDbContext: IdentityDbContext<IdentityUser>
    {
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options): base(options)
        {
        }
        public DbSet<BloodDonorEntity> BloodDonors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<CampaignEntity> Campaigns { get; set; }

        public DbSet<DonorCampaignEntity> DonorCampaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DonorCampaignEntity>()
                    .HasKey(dc => new { dc.BloodDonorId, dc.CampaignId });

            modelBuilder.Entity<DonorCampaignEntity>()
                .HasOne(dc => dc.BloodDonor)
                .WithMany(bd => bd.DonorCampaigns)
                .HasForeignKey(dc => dc.BloodDonorId);

            modelBuilder.Entity<DonorCampaignEntity>()
                .HasOne(dc => dc.Campaign)
                .WithMany(c => c.DonorCampaigns)
                .HasForeignKey(dc => dc.CampaignId);

            modelBuilder.Entity<BloodDonorEntity>()
                .HasData(
                new BloodDonorEntity
                {
                    Id = 1,
                    FullName = "Alice Thomas",
                    ContactNumber = "9876543210",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Email = "alice@example.com",
                    BloodGroup = BloodGroup.APositive,
                    Weight = 60,
                    Address = "New York",
                    LastDonationDate = new DateTime(2024, 12, 1),
                    ProfilePicture = "profiles/alice.jpg"
                },
                new BloodDonorEntity
                {
                    Id = 2,
                    FullName = "Bob Smith",
                    ContactNumber = "9876543211",
                    DateOfBirth = new DateTime(1985, 10, 20),
                    Email = "bob@example.com",
                    BloodGroup = BloodGroup.ONegative,
                    Weight = 72,
                    Address = "Chicago",
                    LastDonationDate = null,
                    ProfilePicture = "profiles/bob.jpg"
                });
        }
    }
}
