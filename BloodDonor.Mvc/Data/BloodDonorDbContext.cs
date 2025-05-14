using BloodDonor.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Data
{
    public class BloodDonorDbContext: DbContext
    {
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options): base(options)
        {
        }
        public DbSet<BloodDonorEntity> BloodDonors { get; set; }
        public DbSet<Donation> Donations { get; set; }       
    }
}
