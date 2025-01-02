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
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        // Sample data model for Sales Order
        public class SalesOrder
        {
            public int OrderID { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public int Quantity { get; set; }
            public string Status { get; set; }
        }

        // List to hold sales orders
        private List<SalesOrder> salesOrders;
        private int orderCounter = 1; // To generate unique Order IDs
        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";
        public Window5()
        {
            InitializeComponent();
            salesOrders = new List<SalesOrder>();
            dgSalesOrders.ItemsSource = salesOrders; // Bind the DataGrid to the list
            LoadSalesOrders(); // Load existing sales orders from the database
        }

        // Method to load sales orders from the database
        private void LoadSalesOrders()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT OrderID, CustomerName, Product, Quantity, Status FROM SalesOrders";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalesOrder order = new SalesOrder
                            {
                                OrderID = reader.GetInt32(0),
                                Customer = reader.GetString(1),
                                Product = reader.GetString(2),
                                Quantity = reader.GetInt32(3),
                                Status = reader.GetString(4)
                            };
                            salesOrders.Add(order);
                        }
                    }
                }
            }
            dgSalesOrders.Items.Refresh(); // Refresh the DataGrid to show the loaded orders
        }

        // Event handler for the Create Sales Order button
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            // Get selected customer and product
            string customer = (cmbCustomers.SelectedItem as ComboBoxItem)?.Content.ToString();
            string product = (cmbProducts.SelectedItem as ComboBoxItem)?.Content.ToString();
            int quantity;

            // Validate quantity input
            if (string.IsNullOrWhiteSpace(customer) || string.IsNullOrWhiteSpace(product) || !int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please select a customer, product, and enter a valid quantity.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new sales order
            SalesOrder newOrder = new SalesOrder
            {
                OrderID = orderCounter++,
                Customer = customer,
                Product = product,
                Quantity = quantity,
                Status = "Pending" // Default status
            };

            // Add the new order to the database
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO SalesOrders (CustomerName, Product, Quantity, Status) VALUES (@customer, @product, @quantity, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customer", customer);
                    command.Parameters.AddWithValue("@product", product);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@status", newOrder.Status);

                    command.ExecuteNonQuery();
                }
            }

            // Add the new order to the list and refresh the DataGrid
            salesOrders.Add(newOrder);
            dgSalesOrders.Items.Refresh(); // Refresh the DataGrid to show the new order

            // Clear the input fields
            cmbCustomers.SelectedIndex = -1;
            cmbProducts.SelectedIndex = -1;
            txtQuantity.Clear();

            MessageBox.Show("Sales Order created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void GoToWindow6_Click(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6(); // Create an instance of Window6
            window6.Show(); // Show Window6
            this.Close(); // Close the current window (Window5)
        }
    }
}
   