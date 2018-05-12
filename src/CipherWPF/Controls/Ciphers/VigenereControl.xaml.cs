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
    	private readonly Vigenere<LetterTextArray> Cipher = new Vigenere<LetterTextArray>();
    	
        public VigenereControl()
        {
            InitializeComponent();

            KeyLength.Minimum = Vigenere<LetterTextArray>.MinKeyLength;
            KeyLength.Maximum = Vigenere<LetterTextArray>.MaxKeyLength;
            KeyLength.Value = Math.Max(Vigenere<LetterTextArray>.MinKeyLength, (int)KeyLength.Value);
        }

        public string Decode(string input)
        {
        	return Cipher.Decode(input, KeyConverters.String.FromString(Key.Text)).ToString();
        }

        public async Task<string> Crack(string Input)
        {
        	ICipherResult<byte[], LetterTextArray> result;
            if (UseKeyLength.IsChecked.HasValue && UseKeyLength.IsChecked.Value)
            {
                int Length = (int)KeyLength.Value;
                result = await Task<ICipherResult<byte[], LetterTextArray>>.Run(() => Cipher.Crack(Input, Length));
            }
            else
            {
                result = await Task<ICipherResult<byte[], LetterTextArray>>.Run(() => Cipher.Crack(Input));
            }

            Key.Text = KeyConverters.String.ToString(result.Key);
            KeyLength.Value = result.Key.Length;
            return result.Contents.ToString();
        }
    	
		public bool CanInvert()
		{
			return true;
		}
    	
		public void Invert()
		{
			Key.Text = KeyConverters.String.ToString(Cipher.Invert(KeyConverters.String.FromString(Key.Text)));
		}
    }
}
