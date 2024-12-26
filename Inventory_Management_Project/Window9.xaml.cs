using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace ReorderAlertsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadInventoryData();
        }

        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public int StockLevel { get; set; }
            public int MinStockLevel { get; set; }
            public string SupplierLink { get; set; }
        }

        private void LoadInventoryData()
        {
            string filePath = "InventoryData.xml"; // Path to the XML file
            XElement inventoryXml = XElement.Load(filePath);

            // Parse the XML and create Product objects
            var products = inventoryXml.Elements("Product")
                .Select(p => new Product
                {
                    ID = (int)p.Element("ID"),
                    Name = (string)p.Element("Name"),
                    Category = (string)p.Element("Category"),
                    StockLevel = (int)p.Element("StockLevel"),
                    MinStockLevel = (int)p.Element("MinStockLevel"),
                    SupplierLink = (string)p.Element("SupplierLink")
                }).ToList();

            // Check for reorder alerts
            var reorderAlerts = products.Where(p => p.StockLevel < p.MinStockLevel)
                                        .Select(p => new
                                        {
                                            p.Name,
                                            p.StockLevel,
                                            p.MinStockLevel,
                                            ReorderSuggestion = $"Stock is below minimum. Reorder from {p.SupplierLink}"
                                        }).ToList();

            // Display the reorder alerts
            ReorderAlertsListView.ItemsSource = reorderAlerts;
        }
    }
}
