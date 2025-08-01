using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.ViewModel
{
    public class CampaignCreateViewModel
    {
        [Required]
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public required string Location { get; set; }
    }
}
