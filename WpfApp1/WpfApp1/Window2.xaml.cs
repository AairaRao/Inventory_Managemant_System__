using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class Window2 : Window
    {
        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";

        public Window2()
        {
            InitializeComponent();
            LoadProducts(); // Load products when the window is initialized
        }

        // Event handler for the Add button
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string sku = txtSKU.Text.Trim();
            string category = (cmbCategory.SelectedItem as ComboBoxItem)?.Content.ToString();
            string priceText = txtPrice.Text.Trim();
            string description = txtDescription.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Products (SKU, Category, UnitPrice, Description) VALUES (@sku, @category, @price, @description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sku", sku);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@description", description);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Item Added:\nSKU: {sku}\nCategory: {category}\nPrice: {price}\nDescription: {description}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadProducts(); // Refresh the product list
        }

        // Event handler for the Edit button
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string sku = txtSKU.Text.Trim();
            string category = (cmbCategory.SelectedItem as ComboBoxItem)?.Content.ToString();
            string priceText = txtPrice.Text.Trim();
            string description = txtDescription.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Products SET Category = @category, UnitPrice = @price, Description = @description WHERE SKU = @sku";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sku", sku);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@description", description);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Item Edited:\nSKU: {sku}\nCategory: {category}\nPrice: {price}\nDescription: {description}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadProducts(); // Refresh the product list
        }

        // Event handler for the Delete button
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string sku = txtSKU.Text.Trim();

            if (string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Please enter a SKU to delete.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Products WHERE SKU = @sku";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sku", sku);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show($"Item Deleted:\nSKU: {sku}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadProducts(); // Refresh the product list
        }

        // Event handler for the Generate Barcode button
        private void GenerateBarcodeButton_Click(object sender, RoutedEventArgs e)
        {
            string sku = txtSKU.Text.Trim();
            if (string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Please enter a SKU to generate a barcode.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Here you would implement the barcode generation logic
            txtBarcode.Text = $"[Barcode for {sku}]"; // Placeholder for generated barcode
        }

        // Method to load products into the DataGrid
        private void LoadProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT SKU, Category, UnitPrice, Description FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                SKU = reader["SKU"].ToString(),
                                Category = reader["Category"].ToString(),
                                UnitPrice = (decimal)reader["UnitPrice"],
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
            }

            
        }

        // Event handler for navigating to the next window
        private void GoToNextForm_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4(); // Create an instance of Window3
            window4.Show(); // Show Window3
            this.Close(); // Close the current window (Window2)
        }

        // Class to represent a product
        public class Product
        {
            public string SKU { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public string Description { get; set; }
        }
    }
}