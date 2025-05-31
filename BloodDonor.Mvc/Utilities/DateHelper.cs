namespace BloodDonor.Mvc.Utilities
{
    public class DateHelper
    {
        public static string GetDays(DateTime? LastDonationDate) => LastDonationDate.HasValue ? $"{(DateTime.Today - LastDonationDate.Value).Days} days ago" : "Never";
    }
}
