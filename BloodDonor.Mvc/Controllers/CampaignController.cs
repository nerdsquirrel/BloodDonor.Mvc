using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ILogger<CampaignController> _logger;
        private readonly BloodDonorDbContext _context;

        public CampaignController(ILogger<CampaignController> logger, BloodDonorDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var campaigns = await _context.Campaigns
                .Select(c => new CampaignListViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Location = c.Location,
                    DonorCount = c.DonorCampaigns.Count
                })
                .ToListAsync();

            return View(campaigns);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CampaignCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var campaign = new CampaignEntity
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Location = model.Location
                };
                _context.Campaigns.Add(campaign);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var campaign = await _context.Campaigns
                .Include(c => c.DonorCampaigns)
                    .ThenInclude(dc => dc.BloodDonor)
                        .ThenInclude(d => d.Donations)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }
            var model = new CampaignDetailsViewModel
            {
                Id = campaign.Id,
                Title = campaign.Title,
                Description = campaign.Description,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Location = campaign.Location,
                Donors = campaign.DonorCampaigns.Select(dc => new BloodDonorInCampaignViewModel
                {
                    Id = dc.BloodDonor.Id,
                    FullName = dc.BloodDonor.FullName,
                    ContactNumber = dc.BloodDonor.ContactNumber,
                    Email = dc.BloodDonor.Email,
                    BloodGroup = dc.BloodDonor.BloodGroup.ToString(),
                    Address = dc.BloodDonor.Address,
                    LastDonationDate = dc.BloodDonor.LastDonationDate?.ToString("yyyy-MM-dd") ?? "N/A",
                    ProfilePicture = dc.BloodDonor.ProfilePicture,
                    DonorCount = dc.BloodDonor.Donations.Count
                }).ToList()
            };
            return View(model);

        }
    }
}
