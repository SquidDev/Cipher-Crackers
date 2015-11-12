using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

using Cipher.Ciphers;
using Cipher.Text;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for VigenereControl.xaml
    /// </summary>
    public partial class ColumnarTranspositionControl : UserControl, IDecode
    {
        private readonly char[] Separators = new char[] { ';', ',' };
        private readonly ColumnarTransposition<CharacterTextArray, char> Cipher = new ColumnarTransposition<CharacterTextArray, char>(TextScorers.ScoreQuadgrams);

        public ColumnarTranspositionControl()
        {
            InitializeComponent();

            // It is done on a per-instance basis.
            KeyLength.Minimum = Cipher.MinKeyLength;
            KeyLength.Maximum = Cipher.MaxKeyLength;
            KeyLength.Value = Math.Max(Cipher.MinKeyLength, (int)KeyLength.Value);
        }

        public string Decode(string input)
        {
            return Cipher.Decode(input, KeyConverters.ByteList.FromString(Key.Text)).ToString();
        }

        public async Task<string> Crack(string input)
        {
        	ICipherResult<byte[], CharacterTextArray> result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                byte length = (byte)KeyLength.Value;
                result = await Task<ICipherResult<byte[], CharacterTextArray>>.Run(() => Cipher.Crack(input, length));
            }
            else
            {
                result = await Task<ICipherResult<byte[], CharacterTextArray>>.Run(() => Cipher.Crack(input));
            }

            Key.Text = KeyConverters.ByteList.ToString(result.Key);
            KeyLength.Value = result.Key.Length;
            return result.Contents.ToString();
        }
    }
}
