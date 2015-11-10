using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Transposition = Cipher.Ciphers.ColumnarTransposition<Cipher.Text.QuadgramScoredCharacterArray, char>;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for VigenereControl.xaml
    /// </summary>
    public partial class ColumnarTranspositionControl : UserControl, IDecode
    {
        char[] Separators = new char[] { ';', ',' };
        public ColumnarTranspositionControl()
        {
            InitializeComponent();

            // It is done on a per-instance basis.
            Transposition Cipher = new Transposition("");
            KeyLength.Minimum = Cipher.MinKeyLength;
            KeyLength.Maximum = Cipher.MaxKeyLength;
            KeyLength.Value = Math.Max(Cipher.MinKeyLength, (int)KeyLength.Value);
        }

        public string Decode(string Input)
        {
            byte[] KeyArray;
            try
            {
                KeyArray = Key.Text.Split(Separators)
                    .Select(S => Convert.ToByte(S.Trim()))
                    .ToArray();
            }
            catch
            {
                return "";
            }

            Transposition Cipher = new Transposition(Input);
            return Cipher.Decode(KeyArray).ToString();

        }

        public async Task<string> Crack(string Input)
        {
            Transposition Cipher = new Transposition(Input);
            Transposition.CipherResult Result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                byte Length = (byte)KeyLength.Value;
                Result = await Task<Transposition.CipherResult>.Run(() => Cipher.Crack(Length));
            }
            else
            {
                Result = await Task<Transposition.CipherResult>.Run(() => Cipher.Crack());
            }

            Key.Text = String.Join(";", Result.Key);
            KeyLength.Value = Result.Key.Length;
            return Result.Text.ToString();
        }
    }
}
