using System.Windows.Controls;
using Cipher.Ciphers;
using Cipher.Text;

namespace Cipher.WPF.Controls
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
            RailFence<QuadgramScoredLetterArray> Cipher = new RailFence<QuadgramScoredLetterArray>(Input);
            return Cipher.Decode((byte)Key.Value).ToString();
        }

        public string Crack(string Input)
        {
            RailFence<QuadgramScoredLetterArray> Cipher = new RailFence<QuadgramScoredLetterArray>(Input);
            RailFence<QuadgramScoredLetterArray>.CipherResult Result = Cipher.Crack();

            Key.Value = Result.Key;
            return Result.Text.ToString();
        }
    }
}