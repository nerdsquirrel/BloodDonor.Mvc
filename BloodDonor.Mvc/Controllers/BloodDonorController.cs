using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonor.Mvc.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BloodDonorController(BloodDonorDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(string bloodGroup, string address)
        {
            IQueryable<BloodDonorEntity> query = _context.BloodDonors;

            if (!string.IsNullOrEmpty(bloodGroup))
                query = query.Where(d => d.BloodGroup.ToString() == bloodGroup);
            if (!string.IsNullOrEmpty(address))
                query = query.Where(d => d.Address != null && d.Address.Contains(address));

            var donors = query.ToList();
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
                LastDonationDate = donor.LastDonationDate
            };
            if(donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(donor.ProfilePicture.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "profiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fullPath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await donor.ProfilePicture.CopyToAsync(stream);                    
                }
                donorEntity.ProfilePicture = Path.Combine("profiles", fileName);
            }
            _context.BloodDonors.Add(donorEntity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
