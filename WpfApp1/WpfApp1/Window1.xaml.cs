using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";

        public Window1()
        {
            InitializeComponent();
        }

        // Event handler for menu item clicks
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Header.ToString())
                {
                    case "New":
                        MessageBox.Show("New file created!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Open":
                        MessageBox.Show("Open file dialog!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Exit":
                        this.Close();
                        break;
                    case "About":
                        MessageBox.Show("Inventory Management System v1.0", "About", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Contact Support":
                        MessageBox.Show("Contact support at support@example.com", "Contact Support", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SidebarButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string buttonContent = button.Content.ToString();
                switch (buttonContent)
                {
                    case "Dashboard":
                        // Do nothing for Dashboard
                        break;
                    case "Product Management":
                        Window3 window3 = new Window3(); // Create an instance of Window3
                        window3.Show(); // Show Window3
                        this.Close(); // Close the current window (Window1)
                        break;
                    case "Purchase Orders":
                        Window4 window4 = new Window4(); // Create an instance of Window4
                        window4.Show(); // Show Window4
                        this.Close(); // Close the current window (Window1)
                        break;
                    case "Create Sales":
                        Window5 window5 = new Window5(); // Create an instance of Window5
                        window5.Show(); // Show Window5
                        this.Close(); // Close the current window (Window1)
                        break;
                    case "Settings":
                        MessageBox.Show("Settings page is under construction.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    default:
                        break;
                }
            }
        }

        private void GoToWindow2_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (ValidateCredentials(username, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GoToWindow2_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND PasswordHash = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password); // Ensure you hash the password in a real application

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                SKU = reader.GetString(2),
                                Category = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Quantity = reader.GetInt32(4),
                                UnitPrice = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                                Barcode = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                UpdatedAt = reader.GetDateTime(8)
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        private void LoadProducts()
        {
            List<Product> products = GetProducts();
            dataGridProducts.ItemsSource = products;
        }

        private List<PurchaseOrder> GetPurchaseOrders()
        {
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM PurchaseOrders";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PurchaseOrder order = new PurchaseOrder
                            {
                                OrderID = reader.GetInt32(0),
                                SupplierID = reader.GetInt32(1),
                                OrderDate = reader.GetDateTime(2),
                                TotalAmount = reader.GetDecimal(3)
                            };
                            purchaseOrders.Add(order);
                        }
                    }
                }
            }
            return purchaseOrders;
        }

        private void LoadPurchaseOrders()
        {
            List<PurchaseOrder> purchaseOrders = GetPurchaseOrders();
            dataGridPurchaseOrders.ItemsSource = purchaseOrders;
        }

        private List<Sales_Orders> GetSales_Orders()
        {
            List<Sales_Orders> salesOrders = new List<Sales_Orders>();
            using (SqlConnection connection = 
            new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SalesOrders";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sales_Orders order = new Sales_Orders
                            {
                                OrderID = reader.GetInt32(0),
                                CustomerID = reader.GetInt32(1),
                                OrderDate = reader.GetDateTime(2),
                                TotalAmount = reader.GetDecimal(3)
                            };
                            salesOrders.Add(order);
                        }
                    }
                }
            }
            return salesOrders;
        }

        private void LoadSales_Orders()
        {
            List<Sales_Orders> salesOrders = GetSales_Orders();
            dataGridSalesOrders.ItemsSource = salesOrders;
        }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PurchaseOrder
    {
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class Sales_Orders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}