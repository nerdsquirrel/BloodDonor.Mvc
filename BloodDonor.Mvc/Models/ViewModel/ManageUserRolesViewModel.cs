namespace BloodDonor.Mvc.Models.ViewModel
{
    public class ManageUserRolesViewModel
    {
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required List<string?> AvailableRoles { get; set; } = new List<string?>();
        public required IList<string> UserRoles { get; set; } = new List<string>();
    }
}
