using AutoMapper;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Interfaces;
using BloodDonor.Mvc.Services.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonor.Mvc.Controllers
{
    [Authorize]
    public class BloodDonorController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IBloodDonorService _bloodDonorService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BloodDonorController> _logger;

        public BloodDonorController(IMapper mapper,
            IFileService fileService,
            IConfiguration configuration,
            ILogger<BloodDonorController> logger,
            IBloodDonorService bloodDonorService)
        {
            _fileService = fileService;
            _bloodDonorService = bloodDonorService;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] FilterDonorModel filter)
        {
            _logger.LogWarning("Fetching blood donors with filter: {@Filter}", filter);
            _logger.LogDebug("Database connection string: {DbConnectionString}", _configuration.GetConnectionString("DefaultConnection"));
            var dbconnectionString = _configuration.GetConnectionString("DefaultConnection");
            var donors = await _bloodDonorService.GetFilteredBloodDonorAsync(filter);
            var donorViewModels = _mapper.Map<List<BloodDonorListViewModel>>(donors);
            return View(donorViewModels);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BloodDonorCreateViewModel donor)
        {
            if(!ModelState.IsValid)
                return View(donor);
            var donorEntity = _mapper.Map<BloodDonorEntity>(donor);
            donorEntity.ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture);
            await _bloodDonorService.AddAsync(donorEntity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if(donor == null)
            {
                return NotFound();
            }            
            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);
            return View(donorViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = _mapper.Map<BloodDonorEditViewModel>(donor);
            return View(donorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BloodDonorEditViewModel donor)
        {
            if (!ModelState.IsValid)
                return View(donor);    
            var donorEntity = _mapper.Map<BloodDonorEntity>(donor);
            donorEntity.ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture) ?? donor.ExistingProfilePicture; 
            await _bloodDonorService.UpdateAsync(donorEntity);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);
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
