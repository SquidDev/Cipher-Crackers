using System.Threading.Tasks;
using System.Windows.Controls;
using Caeser = Cipher.Ciphers.CaeserShift<Cipher.Text.QuadgramScoredLetterArray>;

namespace Cipher.WPF.Controls.Ciphers
{
	/// <summary>
	/// Interaction logic for CaeserShift.xaml
	/// </summary>
	public partial class CaeserShiftControl : UserControl, IDecode
	{
		public CaeserShiftControl()
		{
			this.InitializeComponent();
		}

        public string Decode(string Input)
        {
            Caeser Cipher = new Caeser(Input);
            return Cipher.Decode((byte)Key.Value).ToString();
        }

        public async Task<string> Crack(string Input)
        {
            Caeser.CipherResult Result = await Task<Caeser.CipherResult>.Run(
                () => new Caeser(Input).Crack()
            );

            Key.Value = Result.Key;
            return Result.Text.ToString();
        }
    }
}