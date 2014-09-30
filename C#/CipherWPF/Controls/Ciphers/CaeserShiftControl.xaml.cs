using Cipher.Ciphers;
using Cipher.Text;
using System.Windows.Controls;

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
            CaeserShift<QuadgramScoredLetterArray> Cipher = new CaeserShift<QuadgramScoredLetterArray>(Input);
            return Cipher.Decode((byte)Key.Value).ToString();
        }

        public string Crack(string Input)
        {
            CaeserShift<QuadgramScoredLetterArray> Cipher = new CaeserShift<QuadgramScoredLetterArray>(Input);
            CaeserShift<QuadgramScoredLetterArray>.CipherResult Result = Cipher.Crack();

            Key.Value = Result.Key;
            return Result.Text.ToString();
        }
    }
}