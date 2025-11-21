using System.Windows;

namespace Contract_MC_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Lecturer_Click(object sender, RoutedEventArgs e)
        {
            LecturerDashboard lecturerWindow = new LecturerDashboard();
            lecturerWindow.Show();
        }

        private void Coordinator_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorDashboard coordWindow = new CoordinatorDashboard();
            coordWindow.Show();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerWindow = new ManagerDashboard();
            managerWindow.Show();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //Open Login Window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            //Close MainWindow
            this.Close();
        }
    }
}
