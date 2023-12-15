using System;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace ServerStart
{
    public partial class MainWindow : Window
    {
        private const string ServerUrl = "http://localhost:8080/startMinecraftServer";
        private string parameterValue;
        private string secondParameterValue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            parameterValue = "startServer";
            RequestAndHandleServerAction();
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
                var serverUrlWithParameters = $"{ServerUrl}?parameter={parameterValue}&secondParameter={secondParameterValue}";

                try
                {
                    var response = await client.GetAsync(serverUrlWithParameters);

                    if (response.IsSuccessStatusCode)
                    {
                        var serverResponse = response.Content.ReadAsStringAsync().Result;
                        AppendToOutputTextBox(serverResponse);
                    }
                    else
                    {
                        AppendToOutputTextBox("Fehler beim Ausführen der Aktion auf dem Server.");
                    }
                }
                catch (Exception ex)
                {
                    AppendToOutputTextBox($"Fehler: {ex.Message}");
                }
            }
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SendCommand(object sender, RoutedEventArgs e)
        {
            var command = CommandBox.Text;
            CommandBox.Text = "";
            secondParameterValue = command;
            parameterValue = "command";
            RequestAndHandleServerAction();
        }

        private void AppendToOutputTextBox(string text)
        {
            outputTextBox.AppendText(text + Environment.NewLine);
            outputTextBox.ScrollToEnd();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
