using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp
{
    public partial class NotificationsCenter : Window
    {
        private NotificationManager _notificationManager;

        public NotificationsCenter()
        {
            InitializeComponent();
            _notificationManager = new NotificationManager();
            _notificationManager.InitializeNotifications(); // Initialize notifications
            StartNotificationTimer(); // Start a timer to check for notifications
        }

        // Timer to periodically check for new notifications
        private void StartNotificationTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // Check every 5 seconds
            timer.Tick += (s, e) =>
            {
                var notifications = _notificationManager.GetNotifications();
                NotificationsListBox.ItemsSource = notifications;
                NotificationTextBlock.Text = notifications.LastOrDefault()?.Message ?? "No new notifications.";
            };
            timer.Start();
        }

        // Trigger stock alert example
        private void TriggerStockAlertButton_Click(object sender, RoutedEventArgs e)
        {
            _notificationManager.AddStockAlert("Product 1", 5); // Example stock alert
        }

        // Trigger order status change notification example
        private void TriggerOrderStatusNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            _notificationManager.AddOrderStatusNotification("Order 1234", "Shipped");
        }
    }

    // Notification Class
    public class Notification
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public Notification(string message)
        {
            Timestamp = DateTime.Now;
            Message = message;
        }
    }

    // Notification Manager Class
    public class NotificationManager
    {
        private List<Notification> _notifications;

        public NotificationManager()
        {
            _notifications = new List<Notification>();
        }

        // Initialize with some dummy notifications
        public void InitializeNotifications()
        {
            _notifications.Add(new Notification("System initialized."));
        }

        // Get all notifications
        public List<Notification> GetNotifications()
        {
            return _notifications.OrderByDescending(n => n.Timestamp).ToList(); // Sort by timestamp
        }

        // Add a new stock alert
        public void AddStockAlert(string productName, int stockLevel)
        {
            string message = stockLevel < 10 ? $"{productName} stock is low ({stockLevel} left)." : $"{productName} stock is sufficient.";
            _notifications.Add(new Notification(message));
        }

        // Add an order status notification
        public void AddOrderStatusNotification(string orderId, string newStatus)
        {
            string message = $"Order {orderId} status changed to: {newStatus}.";
            _notifications.Add(new Notification(message));
        }
    }
}
