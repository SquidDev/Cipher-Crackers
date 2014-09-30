using Cipher.Ciphers;
using System;
using System.Linq;
using System.Windows.Controls;
using QSCArray = Cipher.Text.QuadgramScoredCharacterArray;

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
            ColumnarTransposition<QSCArray, char> Cipher = new ColumnarTransposition<QSCArray, char>("");
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
          
            ColumnarTransposition<QSCArray, char> Cipher = new ColumnarTransposition<QSCArray, char>(Input);
            return Cipher.Decode(KeyArray).ToString();

        }

        public string Crack(string Input)
        {
            ColumnarTransposition<QSCArray, char> Cipher = new ColumnarTransposition<QSCArray, char>(Input);
            ColumnarTransposition<QSCArray, char>.CipherResult Result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                Result = Cipher.Crack((byte)KeyLength.Value);
            }
            else
            {
                Result = Cipher.Crack();
            }

            Key.Text = String.Join(";", Result.Key);
            KeyLength.Value = Result.Key.Length;
            return Result.Text.ToString();
        }
    }
}
