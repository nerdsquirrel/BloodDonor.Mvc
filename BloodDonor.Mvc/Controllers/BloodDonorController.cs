using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonor.Mvc.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;

        public BloodDonorController(BloodDonorDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var donors = _context.BloodDonors.ToList();
            return View(donors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodDonorEntity donor)
        {
            ViewBag.Message = "Donor Created Successfully";
            if (ModelState.IsValid)
            {
                // Save the donor to the database
                _context.BloodDonors.Add(donor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = _context.BloodDonors.Find(id);
            if (donors == null)
            {
                return NotFound();
            }
            return View(donors);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.BloodDonorEntity bloodDonor)
        {
            if (id != bloodDonor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(bloodDonor);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(bloodDonor);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonor = _context.BloodDonors
                .FirstOrDefault(m => m.Id == id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            return View(bloodDonor);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var bloodDonor = _context.BloodDonors.Find(id);
            if (bloodDonor != null)
            {
                _context.BloodDonors.Remove(bloodDonor);
            }

            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
