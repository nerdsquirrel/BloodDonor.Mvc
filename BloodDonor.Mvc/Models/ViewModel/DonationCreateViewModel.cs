using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.ViewModel
{
    public class DonationCreateViewModel
    {        
        [Required]
        public int BloodDonorId { get; set; }
        public int? CampaignId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        public string? Location { get; set; }

        public IEnumerable<SelectListItem> Donors { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<SelectListItem> Campaigns { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
