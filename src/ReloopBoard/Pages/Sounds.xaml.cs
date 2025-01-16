using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using NAudio.Wave;
using Microsoft.Win32;

namespace ReloopBoard.Pages
{
    public partial class Sounds : Window
    {
        private string soundsFilePath = "sounds.json";
        private List<Sound> soundsList = new List<Sound>();

        public Sounds()
        {
            InitializeComponent();
            LoadSounds();
            this.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void AddSoundButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*",
                Title = "Select a Sound File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string soundFilePath = openFileDialog.FileName;
                string soundName = System.IO.Path.GetFileName(soundFilePath);

                
                Button soundButton = new Button
                {
                    Content = soundName,
                    Width = 200,
                    Height = 50,
                    Margin = new Thickness(10)
                };

                
                soundButton.Click += (s, args) => PlaySoundButton_Click(s, args, soundFilePath);

                
                SoundsListBox.Items.Add(soundButton);

                
                soundsList.Add(new Sound { Name = soundName, FilePath = soundFilePath });
            }
        }

        
        private void PlaySoundButton_Click(object sender, RoutedEventArgs e, string soundFilePath)
        {
            var waveOut = new WaveOutEvent();

            try
            {
                waveOut.Init(new AudioFileReader(soundFilePath));
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }

        private void SaveSounds()
        {
            string json = JsonConvert.SerializeObject(soundsList, Formatting.Indented);
            File.WriteAllText(soundsFilePath, json);
        }

        private void SaveSoundsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSounds();
            MessageBox.Show("Sounds saved successfully.");
        }


        private void LoadSounds()
        {
            if (File.Exists(soundsFilePath))
            {
                string json = File.ReadAllText(soundsFilePath);
                soundsList = JsonConvert.DeserializeObject<List<Sound>>(json);

                foreach (var sound in soundsList)
                {
                    Button soundButton = new Button
                    {
                        Content = sound.Name,
                        Width = 200,
                        Height = 50,
                        Margin = new Thickness(10)
                    };

                    soundButton.Click += (s, args) => PlaySoundButton_Click(s, args, sound.FilePath);

                    SoundsListBox.Items.Add(soundButton);
                }
            }
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow homeWindow = new MainWindow();
            homeWindow.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
            this.Close();
        }


        private void SoundsButton_Click(object sender, RoutedEventArgs e)
        {
            Sounds soundsWindow = new Sounds();
            soundsWindow.Show();
            this.Close();
        }
    }

    
    public class Sound
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
