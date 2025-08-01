using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonor.Mvc.Models.Entities
{
    public class Donation: BaseEntity
    {
        public required DateTime DonationDate { get; set; }

        [ForeignKey("BloodDonor")]
        public required int BloodDonorId { get; set; }

        public BloodDonorEntity BloodDonor { get; set; } = null!;

        [ForeignKey("Campaign")]
        public int? CampaignId { get; set; }
        public CampaignEntity? Campaign { get; set; }

        public string? Location { get; set; }
    }
}
