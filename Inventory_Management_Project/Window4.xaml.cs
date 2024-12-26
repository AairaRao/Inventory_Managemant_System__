using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PurchaseOrderApp
{
    public partial class PurchaseOrderWindow : Window
    {
        private List<PurchaseOrder> purchaseOrders;
        private List<Supplier> suppliers;

        public PurchaseOrderWindow()
        {
            InitializeComponent();

            // Initialize sample data
            suppliers = new List<Supplier>
            {
                new Supplier { Name = "Supplier A", Contact = "supplierA@example.com" },
                new Supplier { Name = "Supplier B", Contact = "supplierB@example.com" },
                new Supplier { Name = "Supplier C", Contact = "supplierC@example.com" }
            };

            purchaseOrders = new List<PurchaseOrder>();
            SupplierComboBox.ItemsSource = suppliers.Select(s => s.Name);
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            string supplierName = SupplierComboBox.SelectedItem?.ToString();
            int quantity;
            string status = "Pending";

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(supplierName) ||
                !int.TryParse(QuantityTextBox.Text, out quantity))
            {
                MessageBox.Show("Please fill all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var order = new PurchaseOrder
            {
                OrderID = Guid.NewGuid().ToString().Substring(0, 8),
                ProductName = productName,
                SupplierName = supplierName,
                Quantity = quantity,
                OrderStatus = status,
                OrderDate = DateTime.Now
            };

            purchaseOrders.Add(order);
            RefreshOrderList();
            ClearForm();
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListBox.SelectedItem is PurchaseOrder selectedOrder)
            {
                selectedOrder.OrderStatus = "Completed";
                RefreshOrderList();
            }
            else
            {
                MessageBox.Show("Please select an order to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshOrderList()
        {
            OrderListBox.ItemsSource = null;
            OrderListBox.ItemsSource = purchaseOrders.Select(o =>
                $"ID: {o.OrderID} - Product: {o.ProductName} - Supplier: {o.SupplierName} - Quantity: {o.Quantity} - Status: {o.OrderStatus} - Date: {o.OrderDate.ToShortDateString()}");
        }

        private void ClearForm()
        {
            ProductNameTextBox.Clear();
            QuantityTextBox.Clear();
            SupplierComboBox.SelectedIndex = -1;
        }
    }

    public class PurchaseOrder
    {
        public string OrderID { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class Supplier
    {
        public string Name { get; set; }
        public string Contact { get; set; }
    }
}
