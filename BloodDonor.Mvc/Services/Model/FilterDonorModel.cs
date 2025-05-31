namespace BloodDonor.Mvc.Services.Model
{
    public class FilterDonorModel
    {
        public required string bloodGroup { get; set; }
        public required string address { get; set; }
        public bool? isEligible {  get; set; }
    }
}
