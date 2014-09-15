using Cipher.Ciphers;
using Cipher.Text;
using System;
using System.Windows.Controls;

namespace Cipher.WPF.Controls
{
    /// <summary>
    /// Interaction logic for VigenereControl.xaml
    /// </summary>
    public partial class VigenereControl : UserControl, IDecode
    {
        public VigenereControl()
        {
            InitializeComponent();

            // It is done on a per-instance basis.
            MonogramVigenere Cipher = new MonogramVigenere("");
            KeyLength.Minimum = Cipher.MinKeyLength;
            KeyLength.Maximum = Cipher.MaxKeyLength;
            KeyLength.Value = Math.Max(Cipher.MinKeyLength, (int)KeyLength.Value);
        }

        public string Decode(string Input)
        {
            MonogramVigenere Cipher = new MonogramVigenere(Input);
            return Cipher.Decode(new LetterArray(Key.Text)).ToString();
        }

        public string Crack(string Input)
        {
            MonogramVigenere Cipher = new MonogramVigenere(Input);
            MonogramVigenere.CipherResult Result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                Result = Cipher.Crack((int)KeyLength.Value);
            }
            else
            {
                Result = Cipher.Crack();
            }

            Key.Text = Result.Key.ToString();
            KeyLength.Value = Result.Key.Length;
            return Result.Text.ToString();
        }
    }
}
