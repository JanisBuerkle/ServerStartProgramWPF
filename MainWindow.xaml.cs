using System;
using System.Net.Http;
using System.Windows;

namespace ServerStart
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string serverUrl = "http://localhost:8080/startMinecraftServer";
                try
                {
                    HttpResponseMessage response = client.GetAsync(serverUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Lese die Serverantwort
                        string serverResponse = response.Content.ReadAsStringAsync().Result;

                        // Zeige die Serverantwort in einer MessageBox an
                        MessageBox.Show(serverResponse, "Serverantwort");
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Starten des Servers.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }
    }
}