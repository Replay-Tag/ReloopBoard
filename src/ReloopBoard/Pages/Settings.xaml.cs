using System.Windows;
using ReloopBoard;
using NAudio.CoreAudioApi;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace ReloopBoard.Pages
{
    public partial class Settings : Window
    {
        private string settingsFilePath = "audioSettings.json";

        public Settings()
        {
            InitializeComponent();
            LoadAudioDevices();
            LoadSettings();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadAudioDevices()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();

            
            var inputDevices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
            foreach (var device in inputDevices)
            {
                InputSticky.Items.Add(device.FriendlyName);
            }

            
            var outputDevices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
            foreach (var device in outputDevices)
            {
                OutputSticky.Items.Add(device.FriendlyName);
            }

            
            foreach (var device in inputDevices)
            {
                VirtualMicSticky.Items.Add(device.FriendlyName);
            }

            
            if (InputSticky.Items.Count > 0) InputSticky.SelectedIndex = 0;
            if (OutputSticky.Items.Count > 0) OutputSticky.SelectedIndex = 0;
            if (VirtualMicSticky.Items.Count > 0) VirtualMicSticky.SelectedIndex = 0;
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                var json = File.ReadAllText(settingsFilePath);
                var settings = JsonConvert.DeserializeObject<AudioSettings>(json);

                if (settings != null)
                {
                    InputSticky.SelectedItem = settings.InputDevice;
                    OutputSticky.SelectedItem = settings.OutputDevice;
                    VirtualMicSticky.SelectedItem = settings.VirtualMicDevice;
                }
            }
        }

        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new AudioSettings
            {
                InputDevice = InputSticky.SelectedItem?.ToString(),
                OutputDevice = OutputSticky.SelectedItem?.ToString(),
                VirtualMicDevice = VirtualMicSticky.SelectedItem?.ToString()
            };

            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsFilePath, json);
        }

        private void MicrophoneIcon_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow homeWindow = new MainWindow();
            homeWindow.Show();
            this.Close();
        }

        
        private void SoundsButton_Click(object sender, RoutedEventArgs e)
        {
            Sounds soundsWindow = new Sounds();
            soundsWindow.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
            this.Close();
        }
    }
}
