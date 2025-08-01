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

    }
}
