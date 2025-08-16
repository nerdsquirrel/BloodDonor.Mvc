using BloodDonor.Mvc.Data.UnitOfWork;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Services.Interfaces;
using BloodDonor.Mvc.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Services.Implementations
{
    public class BloodDonorService : IBloodDonorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodDonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(BloodDonorEntity bloodDonor)
        {
            _unitOfWork.BloodDonorRepository.Add(bloodDonor);
             await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donor = await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
            if (donor != null)
            {
                _unitOfWork.BloodDonorRepository.Delete(donor);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
        {
            return await _unitOfWork.BloodDonorRepository.GetAllAsync();
        }

        public async Task<BloodDonorEntity?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
        }

        public async Task<List<BloodDonorEntity>> GetFilteredBloodDonorAsync(FilterDonorModel filter)
        {
            var query = _unitOfWork.BloodDonorRepository.Query();

            if (!string.IsNullOrEmpty(filter.bloodGroup))
                query = query.Where(d => d.BloodGroup.ToString() == filter.bloodGroup);

            if (!string.IsNullOrEmpty(filter.address))
                query = query.Where(d => d.Address != null && d.Address.Contains(filter.address));

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(BloodDonorEntity bloodDonor)
        {
            _unitOfWork.BloodDonorRepository.Update(bloodDonor);
            await _unitOfWork.SaveAsync();
        }

        public static bool IsEligible(BloodDonorEntity bloodDonor)
        {
            if (bloodDonor.Weight <= 45 || bloodDonor.Weight >= 200)
                return false;
            if (bloodDonor.LastDonationDate.HasValue)
            {
                var daysSinceLastDonation = (DateTime.Now - bloodDonor.LastDonationDate.Value).TotalDays;
                return daysSinceLastDonation >= 90;
            }
            return true;
        }
    }
}
