using Cipher.Ciphers;
using Cipher.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RFence = Cipher.Ciphers.RailFence<Cipher.Text.QuadgramScoredCharacterArray, char>;

namespace Cipher.WPF.Controls.Ciphers
{
	/// <summary>
	/// Interaction logic for CaeserShift.xaml
	/// </summary>
	public partial class RailFenceControl : UserControl, IDecode
	{
        public RailFenceControl()
		{
			this.InitializeComponent();
		}

        public string Decode(string Input)
        {
            RFence Cipher = new RFence(Input);
            return Cipher.Decode((byte)Key.Value).ToString();
        }

        public async Task<string> Crack(string Input)
        {
            RFence.CipherResult Result = await Task<RFence.CipherResult>.Run(() => new RFence(Input).Crack());

            Key.Value = Result.Key;
            return Result.Text.ToString();
        }
    }
}