using System.ComponentModel.DataAnnotations;

namespace BloodDonor.Mvc.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
