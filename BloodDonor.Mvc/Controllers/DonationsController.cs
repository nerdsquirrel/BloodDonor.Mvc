using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using BloodDonor.Mvc.Models.ViewModel;

namespace BloodDonor.Mvc.Controllers
{
    [Authorize]
    public class DonationsController : Controller
    {
        private readonly BloodDonorDbContext _context;

        public DonationsController(BloodDonorDbContext context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var donations = await _context.Donations
                .Include(d => d.BloodDonor)
                .Include(d => d.Campaign)
                .Select(d => new DonationListViewModel
                {
                    Id = d.Id,
                    DonationDate = d.DonationDate,
                    DonorName = d.BloodDonor.FullName,
                    Campaign = d.Campaign.Title ?? string.Empty,
                    Location = d.Location
                })
                .ToListAsync();
            return View(donations);
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            var donationCreateViewModel = new DonationCreateViewModel
            {
                DonationDate = DateTime.Now,
                Donors = _context.BloodDonors.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.FullName
                }).ToList(),
                Campaigns = _context.Campaigns.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList(),
                CampaignLocations = _context.Campaigns.ToDictionary(c => c.Id.ToString(), c => c.Location.ToString())
            };
            return View(donationCreateViewModel);
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
              var donation = new Donation
              {
                  DonationDate = model.DonationDate,
                  BloodDonorId = model.BloodDonorId,
                  CampaignId = model.CampaignId,
                  Location = model.Location
              };
                _context.Donations.Add(donation);
                
                if(model.CampaignId.HasValue)
                {
                    var campaign = await _context.Campaigns.FindAsync(model.CampaignId.Value);
                    if (campaign != null)
                    {
                        campaign.DonorCampaigns.Add(new DonorCampaignEntity
                        {
                            BloodDonorId = model.BloodDonorId,
                            CampaignId = model.CampaignId.Value
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonationDate,BloodDonorId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.Id == id);
        }
    }
}
