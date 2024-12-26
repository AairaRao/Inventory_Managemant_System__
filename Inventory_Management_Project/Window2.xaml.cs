using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InventoryManagementSystem
{
    public partial class ProductManagementWindow : Window
    {
        private List<Product> products; // List to store product information

        public ProductManagementWindow()
        {
            InitializeComponent();
            products = new List<Product>(); // Initialize the product list
            RefreshProductList(); // Refresh the list on startup
        }

        // Add a new product to the list
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            string category = CategoryComboBox.SelectedItem?.ToString(); // Get selected category
            string barcode = BarcodeTextBox.Text;

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            products.Add(new Product { Name = productName, Category = category, Barcode = barcode });
            RefreshProductList();
            ClearForm();
        }

        // Edit the selected product
        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                selectedProduct.Name = ProductNameTextBox.Text;
                selectedProduct.Category = CategoryComboBox.SelectedItem?.ToString();
                selectedProduct.Barcode = BarcodeTextBox.Text;

                RefreshProductList();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Select a product to edit.", "Edit Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete the selected product
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                products.Remove(selectedProduct);
                RefreshProductList();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Select a product to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Generate a random barcode for the product
        private void GenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            BarcodeTextBox.Text = Guid.NewGuid().ToString("N").Substring(0, 12); // Generates a random 12-character barcode
        }

        // Refresh the product list displayed in the ListBox
        private void RefreshProductList()
        {
            ProductListBox.ItemsSource = null; // Clear the previous list
            ProductListBox.ItemsSource = products.Select(p => $"{p.Name} - {p.Category} - {p.Barcode}");
        }

        // Clear the form fields
        private void ClearForm()
        {
            ProductNameTextBox.Clear();
            BarcodeTextBox.Clear();
            CategoryComboBox.SelectedIndex = -1;
        }
    }

    // Product class to represent individual products
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Barcode { get; set; }
    }
}
