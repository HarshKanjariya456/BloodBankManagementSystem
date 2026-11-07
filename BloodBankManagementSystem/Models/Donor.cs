using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Range(18, 65, ErrorMessage = "Donor age must be between 18 and 65")]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Blood Group")]
        public BloodGroup BloodGroup { get; set; }

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Last Donation Date")]
        public DateTime? LastDonationDate { get; set; }
    }
}