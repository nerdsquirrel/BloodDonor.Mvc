using BloodDonor.Mvc.Repositories.Interfaces;

namespace BloodDonor.Mvc.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloodDonorDbContext _context;

        public UnitOfWork(IBloodDonorRepository bloodDonorRepository, IDonationRepository donationRepository, BloodDonorDbContext context) 
        {
            _context = context;
            BloodDonorRepository = bloodDonorRepository;
            DonationRepository = donationRepository;
        }

        public IBloodDonorRepository BloodDonorRepository { get; }

        public IDonationRepository DonationRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
