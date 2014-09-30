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

        private async void Decode_Click(object sender, System.Windows.RoutedEventArgs e)
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
                catch(Exception Er)
                {
                    Error = Er;
                }

                if(Error != null)
                {
                    await this.ShowMessageAsync("SSomething went wrong, sorry", Error.Message);
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
                catch(Exception Er)
                {
                    Error = Er;
                }
                
                Crack.IsRunning = false;

                if (Error != null)
                {
                    await this.ShowMessageAsync("Something went wrong, sorry", Error.Message);
                }
            }
        }
    }
}
