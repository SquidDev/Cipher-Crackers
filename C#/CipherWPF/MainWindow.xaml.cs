using MahApps.Metro.Controls;
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
                ResultText.Text = Decoder.Decode(InputText.Text);
            }
        }

        private async void Crack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(InputText.Text)) return;

            object Selected = CipherPanels.SelectedContent;
            if (Selected is IDecode)
            {
                IDecode Decoder = (IDecode)Selected;
                ResultText.Text = await Decoder.Crack(InputText.Text);
            }
        }
    }
}
