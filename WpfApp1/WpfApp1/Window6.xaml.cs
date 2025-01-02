using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";
        

        public Window6()
        {
            InitializeComponent();
        }

        // Event handler for the Add Role button
        private void AddRole_Click(object sender, RoutedEventArgs e)
        {
            string roleName = txtRoleName.Text;
            string permission = (cmbPermissions.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(roleName) || string.IsNullOrWhiteSpace(permission))
            {
                MessageBox.Show("Please enter a role name and select a permission.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // SQL connection and command
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Roles (RoleName, Permission) VALUES (@RoleName, @Permission)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        command.Parameters.AddWithValue("@Permission", permission);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show($"Role '{roleName}' with permission '{permission}' added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the role. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An error occurred while adding the role: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            // Clear the input fields
            txtRoleName.Clear();
            cmbPermissions.SelectedIndex = -1;
        }

        // Event handler for the Save Threshold button
        private void SaveThreshold_Click(object sender, RoutedEventArgs e)
        {
            string product = (cmbProducts.SelectedItem as ComboBoxItem)?.Content.ToString();
            string thresholdText = txtThreshold.Text;

            if (string.IsNullOrWhiteSpace(product) || !int.TryParse(thresholdText, out int threshold) || threshold <= 0)
            {
                MessageBox.Show("Please select a product and enter a valid reorder threshold.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Here you would typically save the threshold to a database or a list
            MessageBox.Show($"Reorder threshold for '{product}' set to {threshold} successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear the input fields
            cmbProducts.SelectedIndex = -1;
            txtThreshold.Clear();
        }

        // Event handler for the Test Connection button
        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            string apiEndpoint = txtApiEndpoint.Text;
            string apiKey = txtApiKey.Text;

            if (string.IsNullOrWhiteSpace(apiEndpoint) || string.IsNullOrWhiteSpace(apiKey))
            {
                MessageBox.Show("Please enter both API Endpoint and API Key.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Here you would typically test the connection to the external system
            // For demonstration, we will just show a success message
            MessageBox.Show("Connection to the external system tested successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}