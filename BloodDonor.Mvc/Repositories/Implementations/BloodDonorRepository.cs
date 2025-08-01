using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Repositories.Implementations
{
    public class BloodDonorRepository : Repository<BloodDonorEntity>, IBloodDonorRepository
    {
        public BloodDonorRepository(BloodDonorDbContext context) : base(context)
        {

        }
        public override async Task<BloodDonorEntity?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(b => b.DonorCampaigns)
                    .ThenInclude(dc => dc.Campaign)
                .Include(b => b.Donations)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
