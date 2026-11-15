using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public enum BloodGroup
    {
        [Display(Name = "A+")] APositive = 1,
        [Display(Name = "A-")] ANegative,
        [Display(Name = "B+")] BPositive,
        [Display(Name = "B-")] BNegative,
        [Display(Name = "AB+")] ABPositive,
        [Display(Name = "AB-")] ABNegative,
        [Display(Name = "O+")] OPositive,
        [Display(Name = "O-")] ONegative
    }
}