using System.Collections.ObjectModel;

namespace BloodDonor.Mvc.Models.Entities
{
    public class CampaignEntity: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public Collection<DonorCampaignEntity> DonorCampaigns { get; set; } = new Collection<DonorCampaignEntity>();
    }
}
