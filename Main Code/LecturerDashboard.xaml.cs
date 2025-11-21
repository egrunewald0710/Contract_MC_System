using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Contract_MC_System
{
    public partial class LecturerDashboard : Window
    {
        public static List<LecturerClaim> Claims = new List<LecturerClaim>();

        private readonly Dictionary<string, double> Modules = new Dictionary<string, double>
        {
            { "ICT101 - Introduction to Programming", 350 },
            { "ICT202 - Database Systems", 400 },
            { "ICT303 - Software Engineering", 450 },
            { "ICT404 - Data Structures and Algorithms", 500 },
            { "ICT505 - Cybersecurity Fundamentals", 550 }
        };

        public LecturerDashboard()
        {
            InitializeComponent();
            LoadModules();
            RefreshClaims();
        }

        private void LoadModules()
        {
            cmbModules.ItemsSource = Modules.Keys;
        }

        private void cmbModules_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbModules.SelectedItem != null)
            {
                string selectedModule = cmbModules.SelectedItem.ToString();
                txtHourlyRate.Text = Modules[selectedModule].ToString("F2");
            }
        }

        // Auto-calculate total as hours are typed
        private void txtHoursWorked_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (cmbModules.SelectedItem == null) return;

            if (double.TryParse(txtHoursWorked.Text, out double hours))
            {
                if (hours < 0)
                {
                    MessageBox.Show("Hours cannot be negative.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtHoursWorked.Clear();
                    lblAutoTotal.Content = "Total: R0.00";
                    return;
                }

                string module = cmbModules.SelectedItem.ToString();
                double rate = Modules[module];
                double total = hours * rate;

                lblAutoTotal.Content = $"Total: R{total:F2}";
            }
            else
            {
                lblAutoTotal.Content = "Total: R0.00";
            }
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbModules.SelectedItem == null)
                {
                    MessageBox.Show("Please select a module.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!double.TryParse(txtHoursWorked.Text, out double hours))
                {
                    MessageBox.Show("Please enter valid hours worked.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string moduleName = cmbModules.SelectedItem.ToString();
                double rate = Modules[moduleName];
                double total = hours * rate;

                var newClaim = new LecturerClaim
                {
                    ClaimId = DateTime.Now.ToString("yyyyMMddHHmmss"), // Unique ID
                    ModuleName = moduleName,
                    HoursWorked = hours,
                    HourlyRate = rate,
                    Total = total,
                    Status = "Pending Verification"
                };

                Claims.Add(newClaim);

                MessageBox.Show($"Claim submitted successfully for {moduleName}.\nTotal: R{total:F2}", "Success", MessageBoxButton.OK);

                RefreshClaims();
                cmbModules.SelectedIndex = -1;
                txtHourlyRate.Clear();
                txtHoursWorked.Clear();
                txtUploadedFile.Text = "";
                lblAutoTotal.Content = "Total: R0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Documents|*.pdf;*.docx;*.xlsx"
            };
            if (dlg.ShowDialog() == true)
            {
                txtUploadedFile.Text = $"Uploaded: {System.IO.Path.GetFileName(dlg.FileName)}";
            }
        }

        public void RefreshClaims()
        {
            lstLecturerClaims.Items.Clear();
            foreach (var c in Claims)
                lstLecturerClaims.Items.Add($"[{c.ModuleName}] Claim #{c.ClaimId} - {c.Status} - R{c.Total:F2}");
        }
    }
}
