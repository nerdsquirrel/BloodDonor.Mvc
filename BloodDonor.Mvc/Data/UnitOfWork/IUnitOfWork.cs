using BloodDonor.Mvc.Repositories.Interfaces;

namespace BloodDonor.Mvc.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IBloodDonorRepository BloodDonorRepository { get; }
        IDonationRepository DonationRepository { get; }
        Task<int> SaveAsync();
    }
}
