using System.Windows;
using System.Diagnostics;
using ReloopBoard.Pages;

namespace ReloopBoard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void SoundsButton_Click(object sender, RoutedEventArgs e)
        {
            
            Sounds soundsWindow = new Sounds();
            soundsWindow.Show();

            
            this.Hide();
        }

        
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Hide();
            MainWindow homeWindow = new MainWindow();
            homeWindow.Show();
        }

        
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
            this.Close();
        }

        
        private void GitHubButton_Click(object sender, RoutedEventArgs e)
        {
            
            string url = "https://github.com/Replay-Tag/ReloopBoard";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
