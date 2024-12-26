using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace CustomerManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        // Class to hold customer and order information
        public class Order
        {
            public string OrderID { get; set; }
            public string Product { get; set; }
            public int Quantity { get; set; }
            public DateTime Date { get; set; }
        }

        public class Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public List<Order> Orders { get; set; }
        }

        private void LoadCustomerData()
        {
            string filePath = "Customers.xml"; // Path to the XML file

            // Load the XML file
            XElement customersXml = XElement.Load(filePath);

            // Parse the XML and create Customer objects
            var customers = customersXml.Elements("Customer")
                .Select(c => new Customer
                {
                    ID = (int)c.Element("ID"),
                    Name = (string)c.Element("Name"),
                    Email = (string)c.Element("Email"),
                    Phone = (string)c.Element("Phone"),
                    Address = (string)c.Element("Address"),
                    Orders = c.Elements("Orders").Elements("Order")
                        .Select(o => new Order
                        {
                            OrderID = (string)o.Element("OrderID"),
                            Product = (string)o.Element("Product"),
                            Quantity = (int)o.Element("Quantity"),
                            Date = DateTime.Parse((string)o.Element("Date"))
                        }).ToList()
                }).ToList();

            // Bind the customers to the UI
            CustomersListView.ItemsSource = customers;
        }
    }
}
