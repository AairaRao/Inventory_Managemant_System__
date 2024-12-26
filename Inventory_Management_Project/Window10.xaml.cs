using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace WpfApp
{
    public partial class UserManagement : Window
    {
        private UserManager _userManager;

        public UserManagement()
        {
            InitializeComponent();
            _userManager = new UserManager();
            _userManager.InitializeXmlFile(); // Ensure XML file exists
        }

        // Add user button click handler
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string role = RoleTextBox.Text;
            List<string> permissions = PermissionsTextBox.Text.Split(',').ToList(); // Assuming permissions are comma separated

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                ResultTextBlock.Text = "Please fill all fields.";
                return;
            }

            User newUser = new User(username, password, role, permissions);
            _userManager.AddUser(newUser);
            _userManager.LogUserActivity(username, "Added new user", "User Management");

            ResultTextBlock.Text = $"User {username} added successfully.";
        }

        // Remove user button click handler
        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                ResultTextBlock.Text = "Please enter a username.";
                return;
            }

            try
            {
                _userManager.RemoveUser(username);
                _userManager.LogUserActivity(username, "Removed user", "User Management");

                ResultTextBlock.Text = $"User {username} removed successfully.";
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        // View audit logs button click handler
        private void ViewLogsButton_Click(object sender, RoutedEventArgs e)
        {
            List<AuditLog> logs = _userManager.LoadAuditLogs();
            LogsListBox.Items.Clear();

            foreach (var log in logs)
            {
                LogsListBox.Items.Add($"{log.Timestamp}: {log.Username} - {log.Action} ({log.Module})");
            }
        }
    }

    // User Class
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; }

        public User(string username, string password, string role, List<string> permissions)
        {
            Username = username;
            Password = password;
            Role = role;
            Permissions = permissions ?? new List<string>(); // Ensure permissions is not null
        }
    }

    // UserManager Class
    public class UserManager
    {
        private string _xmlFilePath = "users.xml";
        private string _auditLogFilePath = "audit_logs.xml";

        // Initialize the XML file if it doesn't exist
        public void InitializeXmlFile()
        {
            if (!System.IO.File.Exists(_xmlFilePath))
            {
                XDocument doc = new XDocument(new XElement("Users"));
                doc.Save(_xmlFilePath);
            }

            if (!System.IO.File.Exists(_auditLogFilePath))
            {
                XDocument doc = new XDocument(new XElement("AuditLogs"));
                doc.Save(_auditLogFilePath);
            }
        }

        // Add a new user and log the activity
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            XDocument doc = XDocument.Load(_xmlFilePath);
            XElement newUser = new XElement("User",
                new XElement("Username", user.Username),
                new XElement("Password", user.Password),
                new XElement("Role", user.Role),
                new XElement("Permissions", user.Permissions.Select(p => new XElement("Permission", p)))
            );

            doc.Root.Add(newUser);
            doc.Save(_xmlFilePath);
        }

        // Remove a user by username
        public void RemoveUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");
            }

            XDocument doc = XDocument.Load(_xmlFilePath);
            XElement userElement = doc.Descendants("User").FirstOrDefault(u => u.Element("Username")?.Value == username);
            if (userElement != null)
            {
                userElement.Remove();
                doc.Save(_xmlFilePath);
            }
            else
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }
        }

        // Log user activity to the audit log
        public void LogUserActivity(string username, string action, string module)
        {
            XDocument doc = XDocument.Load(_auditLogFilePath);
            XElement logEntry = new XElement("AuditLog",
                new XElement("Timestamp", DateTime.Now),
                new XElement("Username", username),
                new XElement("Action", action),
                new XElement("Module", module)
            );

            doc.Root.Add(logEntry);
            doc.Save(_auditLogFilePath);
        }

        // Load audit logs from the XML file
        public List<AuditLog> LoadAuditLogs()
        {
            XDocument doc = XDocument.Load(_auditLogFilePath);
            return doc.Descendants("AuditLog")
                      .Select(a => new AuditLog
                      {
                          Timestamp = DateTime.Parse(a.Element("Timestamp")?.Value ?? DateTime.Now.ToString()),
                          Username = a.Element("Username")?.Value,
                          Action = a.Element("Action")?.Value,
                          Module = a.Element("Module")?.Value
                      })
                      .ToList();
        }
    }

    // AuditLog Class
    public class AuditLog
    {
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
    }
}
