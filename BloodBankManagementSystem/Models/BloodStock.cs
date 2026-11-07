using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class BloodStock
    {
        public int Id { get; set; }

        [Display(Name = "Blood Group")]
        public BloodGroup BloodGroup { get; set; }

        [Range(0, 10000)]
        [Display(Name = "Units Available")]
        public int Units { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}