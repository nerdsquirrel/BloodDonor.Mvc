using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Interfaces;
using BloodDonor.Mvc.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonor.Mvc.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        private readonly IFileService _fileService;
        private readonly IBloodDonorService _bloodDonorService;

        public BloodDonorController(BloodDonorDbContext context, IFileService fileService, IBloodDonorService bloodDonorService)
        {
            _context = context;
            _fileService = fileService;
            _bloodDonorService = bloodDonorService;
        }

        public async Task<IActionResult> Index(string bloodGroup, string address, bool? isEligible)
        {
            var filter = new FilterDonorModel { bloodGroup = bloodGroup, address = address, isEligible = isEligible };
            var donors = await _bloodDonorService.GetFilteredBloodDonorAsync(filter);
            return View(donors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BloodDonorCreateViewModel donor)
        {
            if(!ModelState.IsValid)
                return View(donor);
            var donorEntity = new BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                Weight = donor.Weight,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate,
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture)
            };
            _context.BloodDonors.Add(donorEntity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if(donor == null)
            {
                return NotFound();
            }
            var donorViewModel = new BloodDonorListViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                Age = DateTime.Now.Year - donor.DateOfBirth.Year,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup.ToString(),
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
                ProfilePicture = donor.ProfilePicture,
                IsEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorViewModel);
        }

        public IActionResult Edit(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = new BloodDonorEditViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate,
                ExistingProfilePicture = donor.ProfilePicture,
            };
            return View(donorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BloodDonorEditViewModel donor)
        {
            if (!ModelState.IsValid)
                return View(donor);

            var donorEntity = new BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                Weight = donor.Weight,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate,
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture)
            };            
            _context.BloodDonors.Add(donorEntity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = new BloodDonorListViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                Age = DateTime.Now.Year - donor.DateOfBirth.Year,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup.ToString(),
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
                ProfilePicture = donor.ProfilePicture,
                IsEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorViewModel);
        }

        [ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            await _bloodDonorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
