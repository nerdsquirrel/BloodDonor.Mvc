using System.Collections.ObjectModel;

namespace BloodDonor.Mvc.Models.Entities
{
    public class DonorCampaignEntity
    {
        public int BloodDonorId { get; set; }
        public BloodDonorEntity BloodDonor { get; set; } = null!;
        public int CampaignId { get; set; }
        public CampaignEntity Campaign { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
