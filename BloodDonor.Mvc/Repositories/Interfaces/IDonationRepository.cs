using BloodDonor.Mvc.Models.Entities;
using System.Linq.Expressions;

namespace BloodDonor.Mvc.Repositories.Interfaces
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
        void Add(Donation bloodDonor);
        void Update(Donation bloodDonor);
        void Delete(Donation bloodDonor);
    }
}
