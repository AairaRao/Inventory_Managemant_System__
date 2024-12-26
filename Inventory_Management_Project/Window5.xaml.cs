using System;
using System.Linq;
using System.Windows;
using System.Xml;

namespace SalesOrderManagementApp
{
    // MainWindow that contains buttons to open other windows and perform Sales Order actions
    public partial class MainWindow : Window
    {
        private string xmlFilePath = "sales_orders.xml";  // Path to the XML file

        public MainWindow()
        {
            InitializeComponent();
        }

        // Method to load sales orders from the XML file
        private XmlDocument LoadSalesOrders()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);  // Ensure the XML file is in the project directory
            return doc;
        }

        // Method to save sales orders to the XML file
        private void SaveSalesOrders(XmlDocument doc)
        {
            doc.Save(xmlFilePath);
        }

        // Get order details by OrderID
        private void GetOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = LoadSalesOrders();
            int orderId = 1;  // Example order ID, can be dynamically set

            XmlNode orderNode = doc.SelectSingleNode($"/SalesOrders/Order[OrderID='{orderId}']");

            if (orderNode != null)
            {
                ResultTextBlock.Text = $"Order ID: {orderNode["OrderID"].InnerText}\n" +
                                       $"Customer ID: {orderNode["CustomerID"].InnerText}\n" +
                                       $"Customer Name: {orderNode["CustomerName"].InnerText}\n" +
                                       $"Product: {orderNode["Product"].InnerText}\n" +
                                       $"Quantity: {orderNode["Quantity"].InnerText}\n" +
                                       $"Order Status: {orderNode["OrderStatus"].InnerText}\n" +
                                       $"Price: {orderNode["Price"].InnerText}";
            }
            else
            {
                ResultTextBlock.Text = $"Order ID {orderId} not found.";
            }
        }

        // Update order status
        private void UpdateOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = LoadSalesOrders();
            int orderId = 1;  // Example order ID, can be dynamically set
            string newStatus = "Shipped";  // Example status

            XmlNode orderNode = doc.SelectSingleNode($"/SalesOrders/Order[OrderID='{orderId}']");

            if (orderNode != null)
            {
                XmlNode statusNode = orderNode.SelectSingleNode("OrderStatus");
                statusNode.InnerText = newStatus;
                SaveSalesOrders(doc);
                ResultTextBlock.Text = $"Order ID {orderId} status updated to {newStatus}.";
            }
            else
            {
                ResultTextBlock.Text = $"Order ID {orderId} not found.";
            }
        }

        // Track total sales (calculate total sales amount)
        private void TrackTotalSales_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = LoadSalesOrders();
            var orders = doc.SelectNodes("/SalesOrders/Order");

            decimal totalSales = orders.Cast<XmlNode>()
                                       .Sum(order => decimal.Parse(order["Quantity"].InnerText) * decimal.Parse(order["Price"].InnerText));

            ResultTextBlock.Text = $"Total Sales: ${totalSales}";
        }

        // Button click event to open Product Management window
        private void ProductManagementButton_Click(object sender, RoutedEventArgs e)
        {
            // Open Product Management window
            ProductManagementWindow productWindow = new ProductManagementWindow();
            productWindow.Show();
        }

        // Button click event to open Inventory Management window
        private void InventoryManagementButton_Click(object sender, RoutedEventArgs e)
        {
            // Open Inventory Management window
            InventoryWindow inventoryWindow = new InventoryWindow();
            inventoryWindow.Show();
        }
    }

    // Product Management Window (now a proper Window derived class)
    public class ProductManagementWindow : Window
    {
        public ProductManagementWindow()
        {
            // Basic window setup
            this.Title = "Product Management";
            this.Width = 400;
            this.Height = 300;

            // Content of the window (you can add more controls here)
            var textBlock = new System.Windows.Controls.TextBlock()
            {
                Text = "This is the Product Management Window",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            this.Content = textBlock;
        }
    }

    // Inventory Management Window (now a proper Window derived class)
    public class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            // Basic window setup
            this.Title = "Inventory Management";
            this.Width = 400;
            this.Height = 300;

            // Content of the window (you can add more controls here)
            var textBlock = new System.Windows.Controls.TextBlock()
            {
                Text = "This is the Inventory Management Window",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            this.Content = textBlock;
        }
    }
}
