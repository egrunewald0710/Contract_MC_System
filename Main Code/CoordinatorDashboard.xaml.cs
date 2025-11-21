using System.Linq;
using System.Windows;

namespace Contract_MC_System
{
    public partial class CoordinatorDashboard : Window
    {
        public CoordinatorDashboard()
        {
            InitializeComponent();
            RefreshClaims();
        }

        // Refresh with highlights for flagged claims
        private void RefreshClaims()
        {
            lstCoordinatorClaims.Items.Clear();
            foreach (var c in LecturerDashboard.Claims)
            {
                if (c.Status != "Approved (Final)" && c.Status != "Rejected by Manager")
                {
                    var item = new System.Windows.Controls.ListBoxItem();
                    item.Content = $"Claim #{c.ClaimId} - {c.Status} - R{c.Total:F2}";
                    if (c.Status.Contains("Requires Review"))
                        item.Foreground = System.Windows.Media.Brushes.Red;
                    lstCoordinatorClaims.Items.Add(item);
                }
            }
        }

        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoordinatorClaims.SelectedItem == null) return;

            string selected = lstCoordinatorClaims.SelectedItem.ToString();
            var claim = LecturerDashboard.Claims.FirstOrDefault(c => selected.Contains(c.ClaimId));

            if (claim != null)
            {
                claim.Status = "Approved by Coordinator";
                RefreshClaims();
                MessageBox.Show("Claim approved.", "Success");
            }
        }

        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoordinatorClaims.SelectedItem == null) return;

            string selected = lstCoordinatorClaims.SelectedItem.ToString();
            var claim = LecturerDashboard.Claims.FirstOrDefault(c => selected.Contains(c.ClaimId));

            if (claim != null)
            {
                claim.Status = "Rejected by Coordinator";
                RefreshClaims();
                MessageBox.Show("Claim rejected.", "Notice");
            }
        }

        private void EscalateClaim_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoordinatorClaims.SelectedItem == null) return;

            string selected = lstCoordinatorClaims.SelectedItem.ToString();
            var claim = LecturerDashboard.Claims.FirstOrDefault(c => selected.Contains(c.ClaimId));

            if (claim != null)
            {
                claim.Status = "Escalated to Manager";
                RefreshClaims();
                MessageBox.Show("Claim has been escalated to the Manager for final approval.", "Escalated");
            }
        }

        // Automation: Auto-validate claims exceeding allowed hours
        private void AutoValidateClaims_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in LecturerDashboard.Claims.Where(c => c.Status == "Pending Verification"))
            {
                if (c.HoursWorked > 160) // Example rule: max 160 hours/month
                    c.Status = "Requires Review - Exceeds Hours";
            }
            RefreshClaims();
            MessageBox.Show("Claims auto-validated.", "Automation");
        }

        // Automation: Approve all pending claims
        private void ApproveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in LecturerDashboard.Claims.Where(c => c.Status == "Pending Verification"))
                c.Status = "Approved by Coordinator";

            RefreshClaims();
            MessageBox.Show("All pending claims approved automatically.", "Automation");
        }
    }
}
