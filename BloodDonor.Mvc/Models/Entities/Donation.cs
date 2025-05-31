using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.Entities
{
    public class Donation: BaseEntity
    {
        public required DateTime DonationDate { get; set; }

        [ForeignKey("BloodDonor")]
        public required int BloodDonorId { get; set; }
    }
}
