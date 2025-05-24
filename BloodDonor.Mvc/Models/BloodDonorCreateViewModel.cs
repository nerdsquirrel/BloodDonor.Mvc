using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models
{
    public class BloodDonorCreateViewModel
    {
        [Required]
        public required string FullName { get; set; }
        [Phone]
        [Length(10, 15)]
        public required string ContactNumber { get; set; }
        public required DateTime DateOfBirth { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required BloodGroup BloodGroup { get; set; }

        [Range(50, 150)]
        [Display(Name = "Weight (kg)")]
        public float Weight { get; set; }
        public string? Address { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
