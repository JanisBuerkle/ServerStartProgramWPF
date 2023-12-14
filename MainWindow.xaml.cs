using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace ServerStart
{
    public partial class MainWindow : Window
    {
        private const string ServerUrl = "http://localhost:8080/startMinecraftServer";
        private string parameterValue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            parameterValue = "startServer";
            RequestAndHandleServerAction();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void StopServerButtonClick(object sender, RoutedEventArgs e)
        {
            parameterValue = "stopServer";
            RequestAndHandleServerAction();
        }

        private async void RequestAndHandleServerAction()
        {
            using (var client = new HttpClient())
            {
                var serverUrlWithParameter = $"{ServerUrl}?parameter={parameterValue}";

                try
                {
                    var response = await client.GetAsync(serverUrlWithParameter);

                    if (response.IsSuccessStatusCode)
                    {
                        var serverResponse = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show(serverResponse, "Serverantwort");
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Ausführen der Aktion auf dem Server.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}