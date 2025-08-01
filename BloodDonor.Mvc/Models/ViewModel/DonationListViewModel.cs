namespace BloodDonor.Mvc.Models.ViewModel
{
    public class DonationListViewModel
    {
        public int Id { get; set; }
        public DateTime DonationDate { get; set; }
        public required string DonorName { get; set; }
        public string? Campaign { get; set; }
        public string? Location { get; set; }
    }
}
