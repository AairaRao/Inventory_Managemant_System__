using System;
using System.IO;
using System.Windows;

namespace BackupRestoreApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Backup button click event handler
        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            string backupDirectory = BackupDirectoryTextBox.Text;
            if (string.IsNullOrWhiteSpace(backupDirectory))
            {
                MessageBox.Show("Please specify a backup directory.");
                return;
            }

            // Simple backup logic
            try
            {
                // Example backup: copy a file (assuming 'data.txt' exists in the application's directory)
                string sourceFile = "data.txt"; // Replace with actual file path
                string backupFile = Path.Combine(backupDirectory, "backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");

                if (File.Exists(sourceFile))
                {
                    File.Copy(sourceFile, backupFile);
                    MessageBox.Show("Backup completed successfully.");
                }
                else
                {
                    MessageBox.Show("Source file does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during backup: {ex.Message}");
            }
        }

        // Restore button click event handler
        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            string restoreDirectory = RestoreDirectoryTextBox.Text;
            if (string.IsNullOrWhiteSpace(restoreDirectory))
            {
                MessageBox.Show("Please specify a restore directory.");
                return;
            }

            // Simple restore logic
            try
            {
                // Assuming the latest backup file is in the restore directory
                var backupFiles = Directory.GetFiles(restoreDirectory, "backup_*.txt");
                if (backupFiles.Length == 0)
                {
                    MessageBox.Show("No backup files found.");
                    return;
                }

                string latestBackup = backupFiles[0]; // Assuming the first one is the latest
                string restoreFile = "data.txt"; // The original file to restore

                File.Copy(latestBackup, restoreFile, true); // Overwrite the original file
                MessageBox.Show("Restore completed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during restore: {ex.Message}");
            }
        }
    }
}
