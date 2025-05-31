using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Repositories.Interfaces;

namespace BloodDonor.Mvc.Repositories.Implementations
{
    public class BloodDonorRepository : Repository<BloodDonorEntity>, IBloodDonorRepository
    {
        public BloodDonorRepository(BloodDonorDbContext context) : base(context)
        {
        }
    }
}
