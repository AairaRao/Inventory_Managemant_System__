using System;
using System.Collections.Generic;
using System.Linq; // Add this for LINQ functionality
using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    public partial class BarcodeScanning : Window
    {
        private ProductManager _productManager;

        public BarcodeScanning()
        {
            InitializeComponent();
            _productManager = new ProductManager(); // Initialize ProductManager
        }

        // Barcode scanning and automatic product lookup on 'Enter' key press
        private void BarcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string barcode = BarcodeTextBox.Text;
                Product product = _productManager.GetProductByBarcode(barcode);

                if (product != null)
                {
                    ProductInfoListBox.Items.Clear();
                    ProductInfoListBox.Items.Add($"Name: {product.Name}");
                    ProductInfoListBox.Items.Add($"Price: {product.Price:C}");
                    ProductInfoListBox.Items.Add($"Stock: {product.Stock}");
                    ResultTextBlock.Text = $"Product {product.Name} found!";
                }
                else
                {
                    ResultTextBlock.Text = "Product not found!";
                }
            }
        }

        // Lookup product based on barcode
        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            string barcode = BarcodeTextBox.Text;
            Product product = _productManager.GetProductByBarcode(barcode);

            if (product != null)
            {
                ProductInfoListBox.Items.Clear();
                ProductInfoListBox.Items.Add($"Name: {product.Name}");
                ProductInfoListBox.Items.Add($"Price: {product.Price:C}");
                ProductInfoListBox.Items.Add($"Stock: {product.Stock}");
                ResultTextBlock.Text = $"Product {product.Name} found!";
            }
            else
            {
                ResultTextBlock.Text = "Product not found!";
            }
        }

        // Add stock to product
        private void AddStockButton_Click(object sender, RoutedEventArgs e)
        {
            string barcode = BarcodeTextBox.Text;
            Product product = _productManager.GetProductByBarcode(barcode);

            if (product != null)
            {
                product.Stock += 10; // Add 10 units to stock (for example)
                _productManager.UpdateProductStock(product);
                ResultTextBlock.Text = $"Added 10 units to {product.Name}. New stock: {product.Stock}.";
            }
            else
            {
                ResultTextBlock.Text = "Product not found!";
            }
        }
    }

    // Product Class
    public class Product
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string barcode, string name, decimal price, int stock)
        {
            Barcode = barcode;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }

    // ProductManager Class (Simulating a data source for products)
    public class ProductManager
    {
        private List<Product> _products;

        public ProductManager()
        {
            _products = new List<Product>
            {
                new Product("1234567890", "Product 1", 12.99m, 50),
                new Product("0987654321", "Product 2", 7.49m, 100),
                new Product("1122334455", "Product 3", 24.99m, 200)
            };
        }

        // Get product by barcode
        public Product GetProductByBarcode(string barcode)
        {
            return _products.FirstOrDefault(p => p.Barcode == barcode);
        }

        // Update stock for a product
        public void UpdateProductStock(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Barcode == product.Barcode);
            if (existingProduct != null)
            {
                existingProduct.Stock = product.Stock;
            }
        }
    }
}
