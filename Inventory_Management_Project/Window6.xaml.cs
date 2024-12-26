using System;
using System.Collections.Generic;
using System.Windows;

namespace StockMovement
{
    public partial class MainWindow : Window
    {
        // List to store movement history
        private List<StockMovementRecord> movementHistory = new List<StockMovementRecord>();

        public MainWindow()
        {
            InitializeComponent();
            MovementHistoryDataGrid.ItemsSource = movementHistory; // Bind DataGrid to the history list
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (MovementTypeComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Safely get the selected movement type
            string movementType = MovementTypeComboBox.SelectedItem?.ToString() ?? string.Empty;

            // Create a new stock movement record
            var newRecord = new StockMovementRecord
            {
                Type = movementType,
                Quantity = quantity,
                Remarks = RemarksTextBox.Text
            };

            // Add record to the history list and refresh the DataGrid
            movementHistory.Add(newRecord);
            MovementHistoryDataGrid.Items.Refresh();

            // Clear input fields
            MovementTypeComboBox.SelectedItem = null;
            QuantityTextBox.Clear();
            RemarksTextBox.Clear();
        }
    }

    // Class to represent a stock movement record
    public class StockMovementRecord
    {
        public string Type { get; set; }       // IN, OUT, ADJUSTMENT
        public int Quantity { get; set; }     // Quantity of stock
        public string Remarks { get; set; }   // Additional remarks
    }
}

