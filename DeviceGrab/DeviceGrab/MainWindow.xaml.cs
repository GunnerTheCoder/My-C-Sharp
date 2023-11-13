using System;
using System.Management;
using System.Windows;

namespace DeviceInfoTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GatherDeviceInfo();
        }

        private void GatherDeviceInfo()
        {
            AddText("Device Information:\n");

            // Collect system information
            AddText($"Operating System: {Environment.OSVersion.VersionString}\n");
            AddText($"Processor: {GetProcessorInfo()}\n");
            AddText($"RAM: {GetTotalRAM()} GB\n");
            AddText($"Display Resolution: {SystemParameters.PrimaryScreenWidth}x{SystemParameters.PrimaryScreenHeight}\n");

            // Add more information as needed

            // You can add more sections and details based on your requirements
        }

        private string GetProcessorInfo()
        {
            string result = string.Empty;
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    result = item["Name"].ToString();
                }
            }
            return result;
        }

        private string GetTotalRAM()
        {
            string result = string.Empty;
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                foreach (var item in searcher.Get())
                {
                    result = $"{Math.Round(Convert.ToDouble(item["TotalPhysicalMemory"]) / (1024 * 1024 * 1024), 2)}";
                }
            }
            return result;
        }

        private void AddText(string text)
        {
            richTextBox.AppendText(text);
        }
    }
}
