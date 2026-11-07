using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class BloodRequest
    {
        public int Id { get; set; }

        [Required, Display(Name = "Patient Name")]
        public string PatientName { get; set; } = string.Empty;

        [Required, Display(Name = "Hospital Name")]
        public string HospitalName { get; set; } = string.Empty;

        [Required, Display(Name = "Blood Group")]
        public BloodGroup BloodGroup { get; set; }

        [Range(1, 20), Display(Name = "Units Required")]
        public int UnitsRequired { get; set; }

        [Required, Phone, Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
    }
}