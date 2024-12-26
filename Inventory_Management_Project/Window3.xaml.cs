using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InventoryTrackingApp
{
    public partial class InventoryWindow : Window
    {
        private List<InventoryItem> inventoryItems;

        public InventoryWindow()
        {
            InitializeComponent();
            inventoryItems = new List<InventoryItem>
            {
                new InventoryItem { ProductName = "Laptop", Location = "Warehouse A", StockLevel = 20, BatchNumber = "B001", LastMovement = DateTime.Now.AddDays(-2) },
                new InventoryItem { ProductName = "Smartphone", Location = "Warehouse B", StockLevel = 50, BatchNumber = "B002", LastMovement = DateTime.Now.AddDays(-1) },
                new InventoryItem { ProductName = "Headphones", Location = "Warehouse C", StockLevel = 100, BatchNumber = "B003", LastMovement = DateTime.Now.AddDays(-3) }
            };

            RefreshInventoryList();
        }

        private void RefreshInventoryList()
        {
            InventoryListBox.ItemsSource = null;
            InventoryListBox.ItemsSource = inventoryItems.Select(item =>
                $"{item.ProductName} - Location: {item.Location} - Stock: {item.StockLevel} - Batch: {item.BatchNumber} - Last Movement: {item.LastMovement.ToShortDateString()}");
        }

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            string location = LocationTextBox.Text;
            int stockLevel;
            string batchNumber = BatchTextBox.Text;

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(batchNumber) ||
                !int.TryParse(StockTextBox.Text, out stockLevel))
            {
                MessageBox.Show("Please fill all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            inventoryItems.Add(new InventoryItem
            {
                ProductName = productName,
                Location = location,
                StockLevel = stockLevel,
                BatchNumber = batchNumber,
                LastMovement = DateTime.Now
            });

            RefreshInventoryList();
            ClearForm();
        }

        private void ClearForm()
        {
            ProductNameTextBox.Clear();
            LocationTextBox.Clear();
            StockTextBox.Clear();
            BatchTextBox.Clear();
        }
    }

    public class InventoryItem
    {
        public string ProductName { get; set; }
        public string Location { get; set; }
        public int StockLevel { get; set; }
        public string BatchNumber { get; set; }
        public DateTime LastMovement { get; set; }
    }
}
