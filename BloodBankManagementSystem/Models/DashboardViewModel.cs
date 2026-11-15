namespace BloodBankManagementSystem.Models
{
    public class DashboardViewModel
    {
        public int TotalDonors { get; set; }
        public int EligibleDonors { get; set; }
        public int TotalUnits { get; set; }
        public int PendingRequests { get; set; }
        public int ApprovedRequests { get; set; }
        public int RejectedRequests { get; set; }
        public List<BloodStock> Stocks { get; set; } = new();
    }
}