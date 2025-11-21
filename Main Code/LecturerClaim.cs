using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract_MC_System
{
    public class LecturerClaim
    {
        public string ClaimId { get; set; }
        public string ModuleName { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
    }
}
