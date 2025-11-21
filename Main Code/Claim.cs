namespace Contract_MC_System
{
    public class Claim
    {
        public int Id { get; set; } // Primary key
        public string ClaimId { get; set; } = string.Empty;
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double Total { get; set; }
        public string Status { get; set; } = string.Empty;

        public Claim() { } // Required for EF
    }
}
