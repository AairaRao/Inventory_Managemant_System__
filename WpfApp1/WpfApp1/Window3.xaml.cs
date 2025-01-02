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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public class InventoryItem
        {
            public string ProductName { get; set; }
            public string Location { get; set; }
            public int Stock { get; set; }
            public string BatchSerial { get; set; }
            public DateTime LastMovement { get; set; }
        }

        private List<InventoryItem> inventoryItems;
        private const string ConnectionString = "Server=DESKTOP-URTUFPE\\SQLEXPRESS;Database=InventoryManagementSystem;Trusted_Connection=true;";
        public Window3()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            inventoryItems = new List<InventoryItem>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ProductName, Location, Stock, BatchSerial, LastMovement FROM Inventory";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InventoryItem item = new InventoryItem
                            {
                                ProductName = reader["ProductName"].ToString(),
                                Location = reader["Location"].ToString(),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                BatchSerial = reader["BatchSerial"] as string,
                                LastMovement = reader.GetDateTime(reader.GetOrdinal("LastMovement"))
                            };
                            inventoryItems.Add(item);
                        }
                    }
                }
            }

            dgInventory.ItemsSource = inventoryItems; // Assuming dgInventory is your DataGrid
        }

        // Event handler for the Refresh button
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData(); // Reload the data from the database
            MessageBox.Show("Data refreshed!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GoToWindow4_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4(); // Create an instance of Window4
            window4.Show(); // Show Window4
            this.Close(); // Close the current window (Window3)
        }
    }
}
