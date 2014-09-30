using Cipher.Ciphers;
using Cipher.Text;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cipher.WPF.Controls.Ciphers
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

        public async Task<string> Crack(string Input)
        {
            MonogramVigenere Cipher = new MonogramVigenere(Input);
            MonogramVigenere.CipherResult Result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                int Length = (int)KeyLength.Value;
                Result = await Task<MonogramVigenere.CipherResult>.Run(() => Cipher.Crack(Length));
            }
            else
            {
                Result = await Task<MonogramVigenere.CipherResult>.Run(() => Cipher.Crack());
            }

            Key.Text = Result.Key.ToString();
            KeyLength.Value = Result.Key.Length;
            return Result.Text.ToString();
        }
    }
}
