using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Controls;

public class ConsoleOutputRedirector : TextWriter
{
    private readonly TextBox outputTextBox;

    public ConsoleOutputRedirector(TextBox outputTextBox)
    {
        this.outputTextBox = outputTextBox;
        Console.SetOut(this);
    }

    public override void Write(char value)
    {
        base.Write(value);
        Debug.WriteLine($"Received character: {value}");
        outputTextBox.Dispatcher.Invoke(() => outputTextBox.AppendText(value.ToString()));
    }


    public override Encoding Encoding => Encoding.UTF8;
}