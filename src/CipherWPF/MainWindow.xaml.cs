using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;

namespace Cipher.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Decode_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(InputText.Text)) return;

            object Selected = CipherPanels.SelectedContent;
            if (Selected is IDecode)
            {
                IDecode Decoder = (IDecode)Selected;
                Exception Error = null;
                try
                {
                    ResultText.Text = Decoder.Decode(InputText.Text);
                }
                catch (Exception Er)
                {
                    Error = Er;
                }

                if (Error != null)
                {
                    ShowErrorMessage(Error);
                }
            }
        }

        private async void Crack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(InputText.Text)) return;

            object Selected = CipherPanels.SelectedContent;
            if (Selected is IDecode)
            {
                IDecode Decoder = (IDecode)Selected;
                Crack.IsRunning = true;

                Exception Error = null;
                try
                {
                    ResultText.Text = await Decoder.Crack(InputText.Text);
                }
                catch (Exception Er)
                {
                    Error = Er;
                }
                
                Crack.IsRunning = false;

                if (Error != null)
                {
                    ShowErrorMessage(Error);
                }
            }
        }
        
        private void Invert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	IDecode decodable = (IDecode)CipherPanels.SelectedContent;
        	if(decodable.CanInvert())
        	{
        		decodable.Invert();
        	}
        	else
        	{
        		ShowErrorMessage("Cannot invert this cipher");
        	}
        }

        public async void ShowErrorMessage(Exception Error)
        {
            await this.ShowMessageAsync("Whoah!", Error.Message);
        }
        
        public async void ShowErrorMessage(String error)
        {
            await this.ShowMessageAsync("Whoah!", error);
        }
    }
}
