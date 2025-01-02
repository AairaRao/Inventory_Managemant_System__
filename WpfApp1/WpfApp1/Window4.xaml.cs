using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class Window4 : Window
    {
        // Sample data model for Purchase Order
        public class PurchaseOrder
        {
            public int OrderID { get; set; }
            public string Supplier { get; set; }
            public string Product { get; set; }
            public int Quantity { get; set; }
            public string Status { get; set; }
        }

        // Sample data model for Product
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
        }

        // List to hold purchase orders and products
        private List<PurchaseOrder> purchaseOrders;
        private List<Product> products;

        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";

        public Window4()
        {
            InitializeComponent();
            purchaseOrders = new List<PurchaseOrder>();
            products = new List<Product>();
            dgProducts.ItemsSource = products; // Bind the DataGrid to the product list
            LoadProducts(); // Load products from the database
        }

        // Method to load products from the database
        private void LoadProducts()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ProductID, ProductName, Price FROM Products"; // Adjust the table name as needed
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductID = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            };
                            products.Add(product);
                        }
                    }
                }
                dgProducts.Items.Refresh(); // Refresh the DataGrid to show the loaded products
            }
        }

        // Event handler for the Create Purchase Order button
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            // Get selected supplier and product
            string supplier = (cmbSuppliers.SelectedItem as ComboBoxItem)?.Content.ToString();
            string product = (cmbProducts.SelectedItem as ComboBoxItem)?.Content.ToString();
            int quantity;

            // Validate quantity input
            if (string.IsNullOrWhiteSpace(supplier) || string.IsNullOrWhiteSpace(product) || !int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please select a supplier, product, and enter a valid quantity.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new purchase order
            PurchaseOrder newOrder = new PurchaseOrder
            {
                Supplier = supplier,
                Product = product,
                Quantity = quantity,
                Status = "Pending" // Default status
            };

            // Add the new order to the database
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO PurchaseOrders (Supplier, Product, Quantity, Status) VALUES (@supplier, @product, @quantity, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@supplier", supplier);
                    command.Parameters.AddWithValue("@product", product);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@status", newOrder.Status);

                    command.ExecuteNonQuery();
                }
            }

            // Add the new order to the list and refresh the DataGrid
            purchaseOrders.Add(newOrder);
            dgProducts.Items.Refresh(); // Refresh the DataGrid to show the new order

            // Clear the input fields
            cmbSuppliers.SelectedIndex = -1;
            cmbProducts.SelectedIndex = -1;
            txtQuantity.Clear();

            MessageBox.Show("Purchase Order created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}