using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Model;

namespace BloodDonor.Mvc.Services.Interfaces
{
    public interface IBloodDonorService
    {
        Task<IEnumerable<BloodDonorEntity>> GetAllAsync();
        Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter);
        Task<BloodDonorEntity?> GetByIdAsync(int id);
        Task AddAsync(BloodDonorEntity bloodDonor);
        Task UpdateAsync(BloodDonorEntity bloodDonor);
        Task DeleteAsync(int id);
    }
}
