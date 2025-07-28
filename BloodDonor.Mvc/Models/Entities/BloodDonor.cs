using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.Entities
{
    public class BloodDonorEntity: BaseEntity
    {
        public required string FullName { get; set; }
        [Phone]
        [Length(10, 15)]
        public required string ContactNumber { get; set; }
        public required DateTime DateOfBirth { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required BloodGroup BloodGroup { get; set; }
        [Range(50,150)]
        public float Weight { get; set; }
        public string? Address { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public string? ProfilePicture { get; set; }
        public Collection<Donation> Donations { get; set; } = new Collection<Donation>();
        public Collection<DonorCampaignEntity> DonorCampaigns { get; set; } = new Collection<DonorCampaignEntity>();
    }
}
