using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Contract_MC_System
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        // Function to validate password based on criteria
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;        // At least one uppercase
            if (!password.Any(char.IsLower)) return false;        // At least one lowercase
            if (!password.Any(char.IsDigit)) return false;        // At least one number
            if (!password.Any(ch => "!@#$%^&*".Contains(ch))) return false; // At least one special char
            return true;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Check all fields are filled
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate password criteria
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password does not meet the criteria:\n" +
                                "• At least 8 characters\n" +
                                "• At least one uppercase letter\n" +
                                "• At least one lowercase letter\n" +
                                "• At least one number\n" +
                                "• At least one special character (!@#$%^&*)",
                                "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Save credentials to static class
            UserData.Username = username;
            UserData.Password = password;
            UserData.Role = role;

            MessageBox.Show($"Sign up successful! Role: {role}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Optionally navigate to Login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
