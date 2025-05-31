using BloodDonor.Mvc.Data.UnitOfWork;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Repositories.Interfaces;
using BloodDonor.Mvc.Services.Interfaces;
using BloodDonor.Mvc.Services.Model;
using BloodDonor.Mvc.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BloodDonor.Mvc.Services.Implementations
{
    public class BloodDonorService : IBloodDonorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodDonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddAsync(BloodDonorEntity bloodDonor)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<BloodDonorEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodDonorEntity?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BloodDonorRepository.GetByIdAsync(id);
        }

        public async Task<List<BloodDonorListViewModel>> GetFilteredBloodDonorAsync(FilterDonorModel filter)
        {
            var query = (await _unitOfWork.BloodDonorRepository.GetAllAsync()).AsEnumerable();

            if (!string.IsNullOrEmpty(filter.bloodGroup))
                query = query.Where(d => d.BloodGroup.ToString() == filter.bloodGroup);

            if (!string.IsNullOrEmpty(filter.address))
                query = query.Where(d => d.Address != null && d.Address.Contains(filter.address));

            var donors = query.Select(d => new BloodDonorListViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                ContactNumber = d.ContactNumber,
                Age = DateTime.Now.Year - d.DateOfBirth.Year,
                Email = d.Email,
                BloodGroup = d.BloodGroup.ToString(),
                Address = d.Address,
                LastDonationDate = DateHelper.GetDays(d.LastDonationDate),
                ProfilePicture = d.ProfilePicture,
                IsEligible = (d.Weight > 45 && d.Weight < 200) && (!d.LastDonationDate.HasValue || (DateTime.Now - d.LastDonationDate.Value).TotalDays >= 90)
            }).ToList();

            if (filter.isEligible.HasValue)
            {
                donors = donors.Where(x => x.IsEligible == filter.isEligible).ToList();
            }
            return donors;
        }

        public Task UpdateAsync(BloodDonorEntity bloodDonor)
        {
            throw new NotImplementedException();
        }
    }
}
