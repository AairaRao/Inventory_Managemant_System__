using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Sample hardcoded username and password for demonstration
        private const string ValidUsername = "admin";
        private const string ValidPassword = "password123";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the input values
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Validate the credentials
            if (ValidateCredentials(username, password))
            {
                // If valid, show a success message and open the dashboard
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Open the Dashboard window
                Window1 dashboard = new Window1();
                dashboard.Show(); // Show the dashboard window
                this.Close(); // Close the login window
            }
            else
            {
                // If invalid, show an error message
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Check if the provided username and password match the valid ones
            return username == ValidUsername && password == ValidPassword;
        }
    }
}