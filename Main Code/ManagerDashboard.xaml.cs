using System.Linq;
using System.Text;
using System.Windows;

namespace Contract_MC_System
{
    public partial class ManagerDashboard : Window
    {
        public ManagerDashboard()
        {
            InitializeComponent();
            RefreshClaims();
        }

        private void RefreshClaims()
        {
            lstManagerClaims.Items.Clear();
            foreach (var c in LecturerDashboard.Claims)
                lstManagerClaims.Items.Add($"Claim #{c.ClaimId} - {c.Status} - R{c.Total:F2}");
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (lstManagerClaims.SelectedItem == null) return;

            string selected = lstManagerClaims.SelectedItem.ToString();
            var claim = LecturerDashboard.Claims.FirstOrDefault(c => selected.Contains(c.ClaimId));
            if (claim != null)
            {
                claim.Status = "Approved (Final)";
                RefreshClaims();
                MessageBox.Show("Claim approved by Manager.", "Success");
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (lstManagerClaims.SelectedItem == null) return;

            string selected = lstManagerClaims.SelectedItem.ToString();
            var claim = LecturerDashboard.Claims.FirstOrDefault(c => selected.Contains(c.ClaimId));
            if (claim != null)
            {
                claim.Status = "Rejected by Manager";
                RefreshClaims();
                MessageBox.Show("Claim rejected by Manager.", "Notice");
            }
        }

        // Export Approved Claims to CSV on Desktop
        private void ExportApprovedClaims_Click(object sender, RoutedEventArgs e)
        {
            var approvedClaims = LecturerDashboard.Claims
                .Where(c => c.Status == "Approved (Final)")
                .ToList();

            if (!approvedClaims.Any())
            {
                MessageBox.Show("No approved claims to export.", "Notice");
                return;
            }

            var csv = new StringBuilder();
            csv.AppendLine("ClaimId,ModuleName,HoursWorked,HourlyRate,Total,Status");

            foreach (var c in approvedClaims)
                csv.AppendLine($"{c.ClaimId},{c.ModuleName},{c.HoursWorked},{c.HourlyRate},{c.Total},{c.Status}");

            try
            {
                string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "ApprovedClaims.csv");
                System.IO.File.WriteAllText(filePath, csv.ToString());

                MessageBox.Show($"Approved claims exported successfully!\nSaved to: {filePath}", "Export Completed");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error exporting claims: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
