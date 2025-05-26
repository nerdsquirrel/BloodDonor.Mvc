namespace BloodDonor.Mvc.Models
{
    public class BloodDonorEditViewModel: BloodDonorCreateViewModel
    {
        public int Id { get; set; }
        public string? ExistingProfilePicture { get; set; }
    }
}
