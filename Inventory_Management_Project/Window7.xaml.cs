using System;
using System.Collections.Generic;
using System.Windows;

namespace SupplierManagement
{
    public partial class MainWindow : Window
    {
        // List to store supplier information and order history
        private List<Supplier> suppliers = new List<Supplier>();

        public MainWindow()
        {
            InitializeComponent();
            OrderHistoryDataGrid.ItemsSource = suppliers; // Bind DataGrid to supplier list
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(SupplierNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ContactNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailAddressTextBox.Text) ||
                VendorRatingComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Safely parse the selected rating
            if (!int.TryParse(VendorRatingComboBox.SelectedItem?.ToString(), out int rating) || rating < 1 || rating > 5)
            {
                MessageBox.Show("Vendor rating must be a number between 1 and 5.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Add new supplier
            var newSupplier = new Supplier
            {
                Name = SupplierNameTextBox.Text,
                ContactNumber = ContactNumberTextBox.Text,
                Email = EmailAddressTextBox.Text,
                VendorRating = rating,
                OrderHistory = "No orders yet."
            };

            suppliers.Add(newSupplier);
            OrderHistoryDataGrid.Items.Refresh();

            // Clear input fields
            SupplierNameTextBox.Clear();
            ContactNumberTextBox.Clear();
            EmailAddressTextBox.Clear();
            VendorRatingComboBox.SelectedItem = null;
        }
    }

    // Class to represent a supplier
    public class Supplier
    {
        public string Name { get; set; }            // Supplier name
        public string ContactNumber { get; set; }  // Supplier contact number
        public string Email { get; set; }          // Supplier email address
        public int VendorRating { get; set; }      // Vendor rating
        public string OrderHistory { get; set; }   // Order history
    }
}

