using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace DashboardApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private List<string> _realTimeUpdates;

        public MainWindow()
        {
            InitializeComponent();
            _realTimeUpdates = new List<string>();
            LoadDashboardData();
            StartRealTimeUpdates();
        }

        private void LoadDashboardData()
        {
            // Mock data for the dashboard
            StockLevelsText.Text = "150 items in stock";
            LowStockAlertText.Text = "5 items low on stock";
            SalesSummaryText.Text = "$12,000 (Today)";
            PurchaseSummaryText.Text = "$8,000 (Today)";
        }

        private void StartRealTimeUpdates()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5); // Update every 5 seconds
            _timer.Tick += UpdateRealTimeData;
            _timer.Start();
        }

        private void UpdateRealTimeData(object sender, EventArgs e)
        {
            // Generate mock real-time updates
            var newUpdate = $"Update at {DateTime.Now:HH:mm:ss}: Stock levels checked.";
            _realTimeUpdates.Add(newUpdate);

            // Update the ListBox with the latest updates
            RealTimeUpdatesList.ItemsSource = null;
            RealTimeUpdatesList.ItemsSource = _realTimeUpdates;
        }
    }
}
