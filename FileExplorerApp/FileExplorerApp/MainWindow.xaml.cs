using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileExplorerApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDrives();
        }

        private void LoadDrives()
        {
            string[] drives = Directory.GetLogicalDrives();

            foreach (string drive in drives)
            {
                Button button = CreateButton(drive);
                button.Click += DriveButtonClick;
                stackPanel.Children.Add(button);
            }
        }

        private void DriveButtonClick(object sender, RoutedEventArgs e)
        {
            string drive = (string)((Button)sender).Tag;
            LoadDirectories(drive);
        }

        private void LoadDirectories(string path)
        {
            try
            {
                stackPanel.Children.Clear();

                // Made by Gunner Castle
                Button backButton = CreateButton("⬅ Back");
                backButton.Click += BackButtonClick;
                stackPanel.Children.Add(backButton);

                if (string.IsNullOrEmpty(path))
                {
                    locationBar.Text = "Drive Root";
                }
                else
                {
                    string[] directories = Directory.GetDirectories(path);
                    foreach (string directory in directories)
                    {
                        Button button = CreateButton(directory);
                        button.Click += DirectoryButtonClick;
                        stackPanel.Children.Add(button);
                    }

                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        Button button = CreateButton(file);
                        button.Click += DirectoryButtonClick; // Click event to handle both files and directories
                        stackPanel.Children.Add(button);
                    }

                    locationBar.Text = path;
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the directory is denied.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            string currentPath = locationBar.Text;
            string parentPath = Directory.GetParent(currentPath)?.FullName;

            if (!string.IsNullOrEmpty(parentPath))
            {
                LoadDirectories(parentPath);
            }
        }

        private void DirectoryButtonClick(object sender, RoutedEventArgs e)
        {
            string path = (string)((Button)sender).Tag;

            if (File.Exists(path))
            {
                // If the selected item is a file, open it
                OpenFile(path);
            }
            else
            {
                // If the selected item is a directory, load its contents
                LoadDirectories(path);
            }
        }

        private void OpenFile(string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath);

                // Check the file extension and open accordingly
                switch (extension.ToLower())
                {
                    case ".html":
                    case ".htm":
                        // Open HTML files in the default web browser
                        Process.Start(filePath);
                        break;

                    case ".exe":
                        // Run executable files with the default associated program
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                        break;

                    // Add more cases for other file types as needed

                    default:
                        MessageBox.Show($"Unable to open file of type '{extension}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Button CreateButton(string path)
        {
            string displayText = string.IsNullOrEmpty(path) ? "Drive Root" : Path.GetFileName(path);

            return new Button
            {
                Content = displayText,
                Tag = path,
                Width = 200,
                Height = 30,
                Margin = new Thickness(5),
            };
        }
    }
}
